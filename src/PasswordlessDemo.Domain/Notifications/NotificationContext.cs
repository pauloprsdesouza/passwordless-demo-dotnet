
using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordlessDemo.Domain.Notifications
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<Notification> _notifications = new();
        public void AddForbiddenError<TEnum>(TEnum message) where TEnum : Enum
        {
            _notifications.Add(new Notification(NotificationType.Forbidden, message.ToString()));
        }

        public void AddNotFoundError<TEnum>(TEnum message) where TEnum : Enum
        {
            _notifications.Add(new Notification(NotificationType.NotFound, message.ToString()));
        }

        public void AddValidationError<TEnum>(TEnum message) where TEnum : Enum
        {
            _notifications.Add(new Notification(NotificationType.BusinessRules, message.ToString()));
        }

        public bool AreThereForbiddenErros()
        {
            return _notifications.Any(x => x.Type == NotificationType.Forbidden);
        }

        public bool AreThereNotFoundErros()
        {
            return _notifications.Any(x => x.Type == NotificationType.NotFound);
        }

        public bool AreThereValidationErrors()
        {
            return _notifications.Any(x => x.Type == NotificationType.BusinessRules);
        }

        public List<string> GetNotFoundErrors()
        {
            return _notifications.Where(x => x.Type == NotificationType.NotFound).Select(x => x.Message).ToList();
        }

        public List<string> GetValidationErrors()
        {
            return _notifications.Where(x => x.Type == NotificationType.BusinessRules).Select(x => x.Message).ToList();
        }

        public List<string> GetForbiddenErrors()
        {
            return _notifications.Where(x => x.Type == NotificationType.Forbidden).Select(x => x.Message).ToList();
        }

        public void AddValidationError(string message)
        {
            _notifications.Add(new Notification(NotificationType.BusinessRules, message));
        }
    }
}
