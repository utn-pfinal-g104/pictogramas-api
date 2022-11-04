using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PictogramasApi.Configuration
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ILogger<DapperContext> _logger;

        public DapperContext(IConfiguration configuration, ILogger<DapperContext> logger)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnectionMaster");
            _logger = logger;

            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    connection.Open();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Scripts\Script creacion pictogramasdblocal.sql");
                    string script = File.ReadAllText(@path);
                    var statements = SplitSqlStatements(script);
                    foreach (var statement in statements)
                    {
                        connection.Execute(statement);
                        _logger.LogInformation($"Se ejecuto statement del script de creacion de BD");
                    }
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Fallo la ejecucion del script de creacion de BD: {ex.Message}");
            }
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        private static IEnumerable<string> SplitSqlStatements(string sqlScript)
        {
            // Make line endings standard to match RegexOptions.Multiline
            sqlScript = Regex.Replace(sqlScript, @"(\r\n|\n\r|\n|\r)", "\n");

            // Split by "GO" statements
            var statements = Regex.Split(
                    sqlScript,
                    @"^[\t ]*GO[\t ]*\d*[\t ]*(?:--.*)?$",
                    RegexOptions.Multiline |
                    RegexOptions.IgnorePatternWhitespace |
                    RegexOptions.IgnoreCase);

            // Remove empties, trim, and return
            return statements
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\n'));
        }
    }
}
