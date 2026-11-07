using Dapper;
using Npgsql;
using System.Threading.Tasks;
using System.Collections.Generic;
using ATMS.Core;

namespace ATMS.Data.Repositories
{
    public class AuthService
    {
        private readonly string _connectionString;

        public AuthService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> ValidateUserAsync(string username, string rawPassword)
        {
            using var conn = new NpgsqlConnection(_connectionString);

            // 1. Fetch user and role
            const string userSql = @"
                SELECT u.id, u.username, u.password_hash, r.role_name AS RoleName 
                FROM users u
                INNER JOIN roles r ON u.role_id = r.id
                WHERE u.username = @Username AND u.is_active = true;";

            var user = await conn.QuerySingleOrDefaultAsync<dynamic>(userSql, new { Username = username });

            if (user == null || !BCrypt.Net.BCrypt.Verify(rawPassword, user.password_hash))
                return false;

            // 2. Fetch specific granular permissions for this role
            const string permSql = @"
                SELECT permission_code 
                FROM role_permissions 
                WHERE role_id = (SELECT role_id FROM users WHERE id = @Id);";

            var permissions = await conn.QueryAsync<string>(permSql, new { Id = user.id });

            // 3. Store in global session
            UserSession.Login(user.id, user.username, user.RoleName, permissions);

            return true;
        }
    }
}