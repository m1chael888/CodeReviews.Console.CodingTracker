using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.m1chael888.Infrastructure
{
    public interface IDbInitializer
    {
        void Initialize();
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly string _connectionString;

        public DbInitializer(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Initialize()
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                var sql = @"CREATE TABLE IF NOT EXISTS CodingSessions(
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            StartTime TEXT,
                            EndTime TEXT,
                            Duration TEXT
                           );";
                conn.Execute(sql);
            }
        }
    }
}
