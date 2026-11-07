using Dapper;
using Npgsql;

namespace ATMS.Data
{
    public static class DataSeeder
    {
        public static void SeedDefaultUsers(string connectionString)
        {
            using var conn = new NpgsqlConnection(connectionString);

            // Check if any users already exist to prevent duplicate seeding
            int userCount = conn.ExecuteScalar<int>("SELECT COUNT(1) FROM users;");

            if (userCount == 0)
            {
                // Generate secure BCrypt hashes for default passwords
                string atmsHash = BCrypt.Net.BCrypt.HashPassword("atms_admin123");
                string mgmtHash = BCrypt.Net.BCrypt.HashPassword("mgmt_123");
                string adminHash = BCrypt.Net.BCrypt.HashPassword("admin_123");
                string principalHash = BCrypt.Net.BCrypt.HashPassword("principal_123");

                const string sql = @"
                    INSERT INTO users (username, password_hash, role_id) VALUES 
                    ('atms', @atmsHash, 1),
                    ('management', @mgmtHash, 2),
                    ('office_admin', @adminHash, 3),
                    ('principal', @principalHash, 4);";

                conn.Execute(sql, new
                {
                    atmsHash,
                    mgmtHash,
                    adminHash,
                    principalHash
                });
            }
        }
    }
}