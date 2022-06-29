using Neo4j.Driver;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Impl
{
    public class Neo4JMgmt : INeo4JMgmt
    {
        private bool _disposed = false;
        private readonly IDriver _driver;

        public Neo4JMgmt()
        {
            _driver = GraphDatabase.Driver("neo4j+s://c1837971.databases.neo4j.io:7687", AuthTokens.Basic("neo4j", "apTzNLk3BRRKx90SfOpT7JDUmo_tKg5nC4MsdrfmJ3o"));
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
