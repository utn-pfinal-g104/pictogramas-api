using Microsoft.Extensions.Options;
using Neo4j.Driver;
using PictogramasApi.Configuration;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.NoSql
{
    public class Neo4JMgmt : INeo4JMgmt
    {
        private readonly IDriver _driver;
        private IOptions<Neo4JConfig> Neo4JConfig { get; set; }

        public Neo4JMgmt(IOptions<Neo4JConfig> neo4JConfig)
        {
            Neo4JConfig = neo4JConfig;
            _driver = GraphDatabase.Driver(Neo4JConfig.Value.Uri, AuthTokens.Basic(Neo4JConfig.Value.User, Neo4JConfig.Value.Password));
        }

        public async Task<PictogramaGrafo> ObtenerPictograma(int id)
        {
            List<PictogramaGrafo> pictogramas = new List<PictogramaGrafo>();
            var query = @"
                MATCH (p:Pictograma)
                WHERE p.id = $id
                RETURN p";

            var session = _driver.AsyncSession();
            try
            {
                var readResults = await session.ReadTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { id = id });
                    return (await result.ToListAsync());
                });

                foreach (var result in readResults)
                {
                    //pictogramas.Add(new Pictograma { Id = Int32.Parse(result["p.id"].ToString()), Title = result["p.title"].ToString() });
                }
            }
            // Capture any errors along with the query and data for traceability
            catch (Neo4jException ex)
            {
                Console.WriteLine($"{query} - {ex}");
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }

            return pictogramas.First();
        }
    }
}
