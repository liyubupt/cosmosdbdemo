using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using CosmosDBApplication.Model;

namespace CosmosDBApplication.Persistence
{
    public class CosmosDBRepository : ICosmosDBRepository
    {
        private Container _container;

        public CosmosDBRepository(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Policy item)
        {
            await this._container.CreateItemAsync<Policy>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Policy>(id, new PartitionKey(id));
        }

        public async Task<Policy> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Policy> response = await this._container.ReadItemAsync<Policy>(id, new PartitionKey("pc"));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Policy>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Policy>(new QueryDefinition(queryString));
            List<Policy> results = new List<Policy>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Policy item)
        {
            await this._container.UpsertItemAsync<Policy>(item, new PartitionKey(id));
        }
    }
}
