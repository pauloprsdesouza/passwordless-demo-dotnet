using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordlessDemo.Domain.Base
{
    public interface IBaseRepository<TDomain> where TDomain : BaseModel
    {
        Task<TDomain> CreateAsync(TDomain item);
        Task<TDomain> GetAsync(string pk, string sk);
        Task<IEnumerable<TDomain>> GetAllAsync(string pk);
        Task<TDomain> UpdateAsync(TDomain item);
        Task DeleteAsync(TDomain domain);
        Task<List<TDomain>> SaveBatch(List<TDomain> items);
    }
}
