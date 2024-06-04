using AutoMapper;
using Aws.Services.Lib.Services.Notifications.SES;
using Aws.Services.Lib.Services.Notifications.SES.Templates;
using Aws.Services.Lib.Services.Notifications.SNS;
using Aws.Services.Lib.Services.Storage.S3;
using Aws.Services.Lib.Services.Storage.S3.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PasswordlessDemo.Domain.EmailConfiguration;
using PasswordlessDemo.Domain.EmailTemplates;
using PasswordlessDemo.Domain.Notifications;
using PasswordlessDemo.Domain.Users;
using System;
using System.Threading.Tasks;

namespace PasswordlessDemo.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationContext _notification;
        private readonly ISESClient _sesClient;
        private readonly ISNSClient _snsClient;
        private readonly IMemoryCache _memoryCache;
        private readonly IS3Bucket _s3Bucket;
        private readonly S3BucketConfiguration _bucketConfiguration;
        private readonly InstitutionalEmailOptions _institutionalEmailOptions;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, INotificationContext notification, ISESClient sesClient, ISNSClient snsClient, IMemoryCache memoryCache, IS3Bucket s3Bucket, IOptions<S3BucketConfiguration> bucketConfiguration,
                           IOptions<InstitutionalEmailOptions> _emailOptions, IMapper mapper)
        {
            _userRepository = userRepository;
            _notification = notification;
            _sesClient = sesClient;
            _snsClient = snsClient;
            _memoryCache = memoryCache;
            _s3Bucket = s3Bucket;
            _bucketConfiguration = bucketConfiguration.Value ?? throw new ArgumentNullException("S3BucketConfiguration is null");
            _institutionalEmailOptions = _emailOptions.Value ?? throw new ArgumentNullException("InstitutionalEmailOptions is null");
            _mapper = mapper;
        }

        public async Task<User> GetProfile(string email)
        {
            return await ValidateUserByEmail(email);
        }

        public async Task<User> SignIn(User user, string token)
        {
            User userRegistered = await ValidateUserByEmail(user.Email);
            if (userRegistered is null)
            {
                return null;
            }

            bool isValid = await SendOrValidateMFAToken(token, userRegistered);
            if (!isValid)
            {
                return null;
            }

            return userRegistered;
        }

        public async Task<User> SignUp(User user)
        {
            User userRegistered = await _userRepository.GetProfile(user.Email);
            if (userRegistered is not null)
            {
                _notification.AddValidationError(UserError.THIS_USER_ALREADY_EXISTS);
                return null;
            }

            userRegistered = await _userRepository.CreateAsync(user);

            return userRegistered;
        }

        public async Task<User> Update(User user)
        {
            var userRegistered = await _userRepository.GetAsync(user.Id.ToString(), "");
            if (userRegistered is null)
            {
                _notification.AddNotFoundError(UserError.USER_NOT_FOUND);
                return null;
            }

            userRegistered.FirstName = user.FirstName;
            userRegistered.LastName = user.LastName;
            userRegistered.Phone = user.Phone;

            _ = await _userRepository.UpdateAsync(userRegistered);

            return userRegistered;
        }

        public async Task<User> UploadProfileImage(Guid userId, IFormFile image)
        {
            User user = await _userRepository.GetAsync(userId.ToString(), "");
            if (user is null)
            {
                _notification.AddNotFoundError(UserError.USER_NOT_FOUND);
                return null;
            }

            string key = await _s3Bucket.UploadFileAsync(_bucketConfiguration.S3BucketPath, image, $"users/{userId}", true);

            user.ProfileImageUrl = $"{_bucketConfiguration.BaseUrl}/{key}";

            return await _userRepository.UpdateAsync(user);
        }

        private async Task<bool> SendOrValidateMFAToken(string mfaToken, User user)
        {
            if (string.IsNullOrEmpty(mfaToken))
            {
                mfaToken = Guid.NewGuid().ToString("N")[..6].ToUpperInvariant();

                SESTemplateMessage<MfaTemplateModel> message = new()
                {
                    SenderAddress = _institutionalEmailOptions.Security,
                    ReceiversAddress = new() { user.Email },
                    TemplateName = EmailTamplateType.MfaCodeTemplate.ToString(),
                    TemplateModel = new MfaTemplateModel() { UserName = user.FirstName, MfaCode = mfaToken }
                };

                await _sesClient.SendEmailAsync(message);

                _ = _memoryCache.Set(user.Email, mfaToken, TimeSpan.FromMinutes(5));

                return false;
            }

            _ = _memoryCache.TryGetValue(user.Email, out string twoFaTokenRegistered);
            if (twoFaTokenRegistered is null)
            {
                _notification.AddForbiddenError(UserError.INVALID_CREDENTIALS);
                return false;
            }

            if (!twoFaTokenRegistered.Equals(mfaToken))
            {
                _notification.AddForbiddenError(UserError.INVALID_CREDENTIALS);
                return false;
            }

            return true;
        }

        private async Task<User> ValidateUserByEmail(string email)
        {
            User user = await _userRepository.GetProfile(email);
            if (user is null)
            {
                _notification.AddForbiddenError(UserError.INVALID_CREDENTIALS);
                return null;
            }

            return user;
        }
    }
}
