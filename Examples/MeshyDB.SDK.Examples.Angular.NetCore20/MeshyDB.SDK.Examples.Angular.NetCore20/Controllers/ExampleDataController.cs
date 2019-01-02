using MeshyDB.SDK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeshyDB.SDK.Examples.Angular.NetCore20.Controllers
{

    [Route("api/[controller]")]
    public class ExampleDataController : Controller
    {
        private static MeshyDBClient client;

        [HttpGet("{id}")]
        public async Task<ExampleData> Get(string id)
        {
            return await client.Meshes.GetAsync<ExampleData>(id);
        }

        [HttpGet]
        public async Task<PageResult<ExampleData>> GetAll(int? favoriteNumber = null, string name = null, int page = 1, int pageSize = 200)
        {
            IList<Expression<Func<ExampleData, bool>>> predicates = new List<Expression<Func<ExampleData, bool>>>();

            predicates.Add(x => true);

            if (favoriteNumber.HasValue)
            {
                predicates.Add(x => x.FavoriteNumber == favoriteNumber);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                predicates.Add(x => x.Name == name);
            }

            var results = await client.Meshes.SearchAsync<ExampleData>(predicates, page, pageSize);

            return results;
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await client.Meshes.DeleteAsync<ExampleData>(id);
        }

        [HttpPost]
        public async Task Create([FromBody] ExampleData model)
        {
            await client.Meshes.CreateAsync<ExampleData>(model);
        }

        internal static void InitializeMeshy(string clientKey, string publicKey, string privateKey)
        {
            client = new MeshyDBClient(clientKey, publicKey, privateKey);
        }

        [HttpPut("{id}")]
        public async Task Update(string id, [FromBody] ExampleData model)
        {
            await client.Meshes.UpdateAsync<ExampleData>(id, model);
        }

        public class ExampleData : MeshData
        {
            // Your Mesh Data Properties will go here

            #region Example Properties

            public string Name { get; set; }
            public int FavoriteNumber { get; set; }

            #endregion
        }
    }
}
