using System.Collections.Generic;
using System.Threading.Tasks;
using CosmosDBApplication.Model;

namespace CosmosDBApplication.Persistence
{
    public interface ICosmosDBRepository
    {
        Task<IEnumerable<Policy>> GetItemsAsync(string query);
        Task<Policy> GetItemAsync(string id);
        Task AddItemAsync(Policy item);
        Task UpdateItemAsync(string id, Policy item);
        Task DeleteItemAsync(string id);
    }
}
