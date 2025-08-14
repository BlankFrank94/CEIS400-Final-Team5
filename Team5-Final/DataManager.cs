using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Team5_Final.Data
{
    public class DataManager
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["Team5_Final.Properties.Settings.CEIS400Team5DBConnectionString"].ConnectionString;

        public DataManager() { 
            EnsureSchema();
        }


        private void EnsureSchema()
        {
            try
            {
                using (var cn = Conn())
                {
                    cn.Open();

                    // Look up columns on EquipmentLogTable, add column for DateReturned
                    var cols = cn.GetSchema("COLUMNS", new[] { null, null, "EquipmentLogTable", null });

                    bool hasDateReturned = false;
                    foreach (System.Data.DataRow r in cols.Rows)
                    {
                        var colName = r["COLUMN_NAME"] as string;
                        if (string.Equals(colName, "DateReturned", StringComparison.OrdinalIgnoreCase))
                        {
                            hasDateReturned = true;
                            break;
                        }
                    }

                    if (!hasDateReturned)
                    {
                        using (var cmd = new OleDbCommand(
                            "ALTER TABLE EquipmentLogTable ADD COLUMN DateReturned DATETIME", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch
            {
                // Don’t crash the app if the check fails
                // see the original SQL error, which tells us what to fix next.
            }
        }

        private OleDbConnection Conn()
        {
            return new OleDbConnection(_cs);
        }
        //TEMP debug: Does the table contain what we think it does??
        public void DebugDump()
        {
            // Where does |DataDirectory| resolve to?
            var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory") as string
                          ?? AppDomain.CurrentDomain.BaseDirectory;

            using (var cn = new OleDbConnection(_cs))
            {
                cn.Open();

                // List user tables
                var tbls = cn.GetSchema("Tables");
                var names = tbls.Rows
                    .Cast<System.Data.DataRow>()
                    .Where(r => string.Equals(r["TABLE_TYPE"]?.ToString(), "TABLE", StringComparison.OrdinalIgnoreCase))
                    .Select(r => r["TABLE_NAME"]?.ToString())
                    .OrderBy(n => n)
                    .ToArray();

                var msg =
        $@"ConnStr:
{_cs}

|DataDirectory|:
{dataDir}

Tables found:
{string.Join(Environment.NewLine, names)}";

                System.Windows.Forms.MessageBox.Show(msg, "DB Debug");
            }
        }


        public DataTable GetEmployees()
        {
            // TEMP debug: where does DataDirectory point?
            var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory") as string
                          ?? AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = Path.Combine(dataDir, "CEIS400Team5DB.accdb");

            if (!File.Exists(dbPath))
                MessageBox.Show($"DB NOT FOUND:\n{dbPath}\n\nConnStr:\n{_cs}");

            // Employees list
            const string sql = @"
                SELECT EmployeeID, FirstName, LastName, SkillLevel
                FROM [Employees Table]
                ORDER BY LastName, FirstName";

            using (var cn = Conn())
            using (var da = new OleDbDataAdapter(sql, cn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetActiveCheckouts()
        {
            // Active checkouts grid
            const string sql = @"
                SELECT  l.LogID,
                    l.EmployeeID,
                    emp.FirstName & ' ' & emp.LastName AS Employee,
                    l.EquipmentID,
                    eq.[EquipmentName] AS Equipment,
                    l.DateCheckedOut
                FROM (EquipmentLogTable AS l
                    INNER JOIN [Employees Table] AS emp ON emp.EmployeeID = l.EmployeeID)
                    INNER JOIN [Equipment Table] AS eq ON eq.EquipmentID = l.EquipmentID
                WHERE l.DateReturned IS NULL
                ORDER BY l.DateCheckedOut DESC;";

            using (var cn = Conn())
            using (var da = new OleDbDataAdapter(sql, cn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAvailableEquipment()
        {
            // Available equipment (anything NOT currently out)
            const string sql = @"
                SELECT  e.EquipmentID,
                    e.[EquipmentName] AS Name,
                    e.[MinSkillLevel]
                FROM [Equipment Table] AS e
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM EquipmentLogTable AS l
                    WHERE l.EquipmentID = e.EquipmentID
                        AND l.DateReturned IS NULL
                )
                ORDER BY e.[EquipmentName];";

            using (var cn = Conn())
            using (var da = new OleDbDataAdapter(sql, cn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


        public int GetEmployeeSkill(string employeeId)
        {
            // Skill for an employee
            const string sql = @"SELECT SkillLevel FROM [Employees Table] WHERE EmployeeID = ?";
            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@p1", employeeId);
                cn.Open();
                object o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? 0 : Convert.ToInt32(o);
            }
        }

        public int GetToolMinSkill(int equipmentId)
        {
            // Min skill for a tool
            const string sql = @"SELECT MinSkillLevel FROM [Equipment Table] WHERE EquipmentID = ?";
            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@p1", equipmentId);
                cn.Open();
                object o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? 0 : Convert.ToInt32(o);
            }
        }



        public bool IsToolCheckedOut(int equipmentId)
        {
            // Is tool currently checked out?
            const string sql = @"
                SELECT COUNT(*)
                FROM EquipmentLogTable
                WHERE EquipmentID = ? AND DateReturned IS NULL";

            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@p1", equipmentId);
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public void Checkout(string employeeId, int equipmentId, DateTime when)
        {
            // Insert a checkout
            const string sql = @"
                INSERT INTO EquipmentLogTable (EmployeeID, EquipmentID, DateCheckedOut)
                VALUES (?,?,?)";

            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@p1", employeeId);
                cmd.Parameters.AddWithValue("@p2", equipmentId);
                cmd.Parameters.AddWithValue("@p3", when);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Return(int logId, DateTime when)
        {
            // Return a tool
            const string sql = @"
                UPDATE EquipmentLogTable
                SET DateReturned = ?
                WHERE LogID = ? AND DateReturned IS NULL";

            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@p1", when);
                cmd.Parameters.AddWithValue("@p2", logId);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
