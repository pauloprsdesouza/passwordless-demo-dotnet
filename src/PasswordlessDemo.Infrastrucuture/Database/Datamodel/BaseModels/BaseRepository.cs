using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoMapper;
using PasswordlessDemo.Domain.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordlessDemo.Infrastructure.Database.Datamodel.BaseModels
{
    public class BaseRepository<TDomain, TModel> : IBaseRepository<TDomain>
                                                   where TDomain : BaseModel
                                                   where TModel : DynamoBaseModel
    {
        public readonly IDynamoDBContext _context;
        public readonly IMapper _mapper;

        public BaseRepository(IDynamoDBContext dynamoDbClient, IMapper mapper)
        {
            _context = dynamoDbClient;
            _mapper = mapper;
        }

        public async Task<TDomain> CreateAsync(TDomain item)
        {
            item.Id = Guid.NewGuid();
            item.CreatedAt = DateTimeOffset.UtcNow;

            TModel model = _mapper.Map<TModel>(item);

            await _context.SaveAsync(model);

            return item;
        }

        public async Task DeleteAsync(TDomain item)
        {
            TModel model = _mapper.Map<TModel>(item);

            await _context.DeleteAsync(model);
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync(string pk)
        {
            var config = new DynamoDBOperationConfig()
            {
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition("PK", ScanOperator.Equal, pk)
                }
            };

            List<TModel> models = await _context.QueryAsync<TModel>(pk, config).GetRemainingAsync();

            return _mapper.Map<List<TDomain>>(models);
        }

        public async Task<TDomain> GetAsync(string pk, string sk)
        {
            TModel model = await _context.LoadAsync<TModel>(pk, sk);

            return _mapper.Map<TDomain>(model);
        }

        public async Task<TDomain> UpdateAsync(TDomain item)
        {
            item.UpdatedAt = DateTimeOffset.UtcNow;

            TModel model = _mapper.Map<TModel>(item);

            await _context.SaveAsync(model);

            return item;
        }

        public async Task<List<TDomain>> SaveBatch(List<TDomain> items)
        {
            List<TModel> models = _mapper.Map<List<TModel>>(items);

            var batch = _context.CreateBatchWrite<TModel>();
            batch.AddPutItems(models);
            await batch.ExecuteAsync();

            return items;
        }
    }
}
