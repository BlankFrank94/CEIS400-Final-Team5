using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team5_Final
{
    public static class CurrentUser
    {
        public static string EmployeeId { get; private set; }
        public static string Name { get; private set; }
        public static string Role { get; private set; }

        public static bool IsAdmin
        {
            get { return string.Equals(Role, "Admin", StringComparison.OrdinalIgnoreCase); }
        }

        public static void Set(string employeeId, string name, string role)
        {
            EmployeeId = employeeId;
            Name = name;
            Role = role ?? "User";
        }

        public static void Clear()
        {
            EmployeeId = null;
            Name = null;
            Role = null;
        }
    }
}