using MeshyDB.SDK.CSharpGuide.Meshes;
using MeshyDB.SDK.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeshyDB.SDK.CSharpGuide.Controllers
{
    [Produces("application/json")]
    [Route("api/Persons")]
    public class PersonsController : Controller
    {
        public static MeshyDBClient client;

        public static void InitializeMeshyDBClient(string clientKey, string publicKey, string privateKey)
        {
            client = new MeshyDBClient(clientKey, publicKey, privateKey);
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<PageResult<Person>> Get(int? age = null, string name = null, int page = 1, int pageSize = 200)
        {
            var filters = new List<Expression<Func<Person, bool>>>();
            if (age.HasValue)
            {
                filters.Add(x => x.Age == age);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                filters.Add(x => x.Name == name);
            }

            return await client.Meshes.SearchAsync(filters, page, pageSize);
        }

        // GET: api/Persons/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Persons
        [HttpPost]
        public async Task<Person> Post([FromBody]Person data)
        {
            return await client.Meshes.CreateAsync(data);
        }

        // PUT: api/Persons/5
        [HttpPut("{id}")]
        public async Task<Person> Put(string id, [FromBody]Person data)
        {
            return await client.Meshes.UpdateAsync(id, data);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await client.Meshes.DeleteAsync<Person>(id);
        }
    }
}
