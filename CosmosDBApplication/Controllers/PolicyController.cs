using CosmosDBApplication.Model;
using CosmosDBApplication.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosDBApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly ICosmosDBRepository _cosmosDbRepository;
        public PolicyController(ICosmosDBRepository cosmosDBRepository)
        {
            _cosmosDbRepository = cosmosDBRepository;
        }

        [HttpGet]
        [ActionName("Index")]
        public async Task<IEnumerable<Policy>> Index()
        {
            return await _cosmosDbRepository.GetItemsAsync("SELECT * FROM c");
        }

        [HttpGet("{policyId}")]
        [ActionName("GetPolicyByID")]
        public async Task<Policy> GetPolicyByID(string policyId)
        {
            Policy policy = await _cosmosDbRepository.GetItemAsync(policyId);
            return policy;
        }
    }
}