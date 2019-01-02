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

            var expr = PredicateBuilder.CombineExpressions(predicates);

            var results = await client.Meshes.SearchAsync<ExampleData>(expr, page, pageSize);

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

        internal static void InitializeMeshy(IConfiguration configuration)
        {
            throw new NotImplementedException();
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
    // https://stackoverflow.com/questions/20380078/having-trouble-aggregating-over-a-list-of-expressions-with-expression-andalso
    internal class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;
        public ReplaceVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }


    public static class PredicateBuilder
    {
        public static Expression Replace(this Expression expression,
    Expression searchEx, Expression replaceEx)
        {
            return new ReplaceVisitor(searchEx, replaceEx).Visit(expression);
        }

        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var secondBody = expr2.Replace(expr2.Parameters[0], expr1.Parameters[0]);
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, secondBody), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var secondBody = expr2.Replace(expr2.Parameters[0], expr1.Parameters[0]);
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, secondBody), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> CombineExpressions<T>(
            IEnumerable<Expression<Func<T, bool>>> expressions)
        {
            if (expressions == null || expressions.Count() == 0)
            {
                return t => true;
            }
            ParameterExpression param = Expression.Parameter(typeof(T));
            var combined = expressions
                            .Select(func => func.Body.Replace(func.Parameters[0], param))
                            .Aggregate((a, b) => Expression.AndAlso(a, b));

            return Expression.Lambda<Func<T, bool>>(combined, param);
        }
    }
}
