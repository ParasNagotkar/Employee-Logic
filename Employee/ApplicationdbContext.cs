using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Employee
{
    public class ApplicationdbContext : DbContext
    {
        public ApplicationdbContext(DbContextOptions<ApplicationdbContext> options) : base(options)
        {
        }

        public DbSet<Employeecs> employees { get; set; }

        public List<Dictionary<string, string>> ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
        {
            var result = new List<Dictionary<string, string>>();

            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, string>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.GetValue(i).ToString();
                        }

                        result.Add(row);
                    }
                }
            }
            return result;
        }


    }
}
