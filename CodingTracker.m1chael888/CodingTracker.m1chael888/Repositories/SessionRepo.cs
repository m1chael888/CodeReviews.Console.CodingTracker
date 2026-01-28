using CodingTracker.m1chael888.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.m1chael888.Repositories
{
    public interface ISessionRepo
    {
        void Create(SessionModel Model);
        List<SessionModel> Read();
        void Update(SessionModel Model);
        void Delete(long id);
    }
    public class SessionRepo : ISessionRepo
    {
        private readonly string _connectionString;

        public SessionRepo(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Create(SessionModel Model)
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                var sql = $"INSERT INTO CodingSessions(StartTime, EndTime, Duration) VALUES($startTime, $endTime, $duration);";

                conn.Execute(sql, new
                {
                    startTime = Model.StartTime,
                    endTime = Model.EndTime,
                    duration = Model.Duration
                });
            }
        }

        public List<SessionModel> Read()
        {
            var sessions = new List<SessionModel>();
            var sql = $"SELECT * FROM CodingSessions";

            using (var conn = new SqliteConnection(_connectionString))
            {
                sessions = conn.Query<SessionModel>(sql).ToList();
            }
            return sessions;
        }

        public void Update(SessionModel Model)
        {
            var sql = $"UPDATE CodingSessions SET StartTime = @startTime, EndTime = @endTime, Duration = @duration WHERE Id = @id";

            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Execute(sql, new 
                {
                    startTime = Model.StartTime,
                    endTime = Model.EndTime,
                    duration = Model.Duration,
                    id = Model.Id
                });
            };
        }

        public void Delete(long id)
        {
            var sql = $"DELETE FROM CodingSessions WHERE Id = @id";

            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Execute(sql, new
                {
                    id = id
                });
            }
        }
    }
}
