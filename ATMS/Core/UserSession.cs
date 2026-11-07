using System.Collections.Generic;

namespace ATMS.Core
{
    public static class UserSession
    {
        public static int UserId { get; private set; }
        public static string Username { get; private set; } = string.Empty;
        public static string RoleName { get; private set; } = string.Empty;
        private static HashSet<string> _permissions = new();

        public static void Login(int id, string user, string role, IEnumerable<string> perms)
        {
            UserId = id;
            Username = user;
            RoleName = role;
            _permissions = new HashSet<string>(perms);
        }

        public static void Logout()
        {
            UserId = 0;
            Username = string.Empty;
            RoleName = string.Empty;
            _permissions.Clear();
        }

        // The UI will call this to enable/disable buttons
        public static bool HasPermission(string permissionCode)
        {
            return _permissions.Contains(permissionCode);
        }
    }
}