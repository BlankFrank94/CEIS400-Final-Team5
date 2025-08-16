using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Team5_Final.Data
{
    public class DataManager
    {
        // Connection string to the Access DB (from App.config)
        private readonly string _cs =
            ConfigurationManager.ConnectionStrings["Team5_Final.Properties.Settings.CEIS400Team5DBConnectionString"]
            .ConnectionString;

        public DataManager()
        {
            EnsureSchema();
        }

        private static string Sha256(string text)
        {
            if (text == null) text = string.Empty;
            using (var sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (var b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public int BackfillRandomCheckoutDates(int maxDays = 21)
        {
            const string SEL = @"
                SELECT LogID
                FROM EquipmentLogTable
                WHERE DateCheckedOut IS NULL AND DateReturned IS NULL";

            const string UPD = @"
                UPDATE EquipmentLogTable
                SET DateCheckedOut = ?
                WHERE LogID = ?";

            using (var cn = Conn())
            using (var cmdSel = new OleDbCommand(SEL, cn))
            using (var cmdUpd = new OleDbCommand(UPD, cn))
            {
                cmdUpd.Parameters.Add("@p1", OleDbType.Date);
                cmdUpd.Parameters.Add("@p2", OleDbType.Integer);

                cn.Open();

                var ids = new System.Collections.Generic.List<int>();
                using (var rd = cmdSel.ExecuteReader())
                    while (rd.Read())
                        ids.Add(Convert.ToInt32(rd["LogID"]));

                var rnd = new Random();
                int updated = 0;

                foreach (var id in ids)
                {
                    var when = DateTime.Now
                        .AddDays(-rnd.Next(1, maxDays + 1))
                        .AddHours(-rnd.Next(0, 24));

                    cmdUpd.Parameters[0].Value = when;
                    cmdUpd.Parameters[1].Value = id;
                    updated += cmdUpd.ExecuteNonQuery();
                }
                return updated;
            }
        }

        // ----------------- Auth -----------------

        public bool TryLoginByEmployeeId(
            string employeeId, string password,
            out string fullName, out string role, out string message)
        {
            fullName = null;
            role = "User";
            message = "Invalid credentials.";

            if (string.IsNullOrWhiteSpace(employeeId) || string.IsNullOrEmpty(password))
            {
                message = "Select an employee and enter a password.";
                return false;
            }

            const string sql = @"
                SELECT FirstName, LastName, Role
                FROM [Employees Table]
                WHERE EmployeeID = ? AND [Password] = ?";

            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                // OleDb parameters are positional – keep this order.
                cmd.Parameters.Add("p1", OleDbType.VarChar).Value = employeeId;
                cmd.Parameters.Add("p2", OleDbType.VarChar).Value = password;

                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return false;

                    var first = Convert.ToString(rd["FirstName"]);
                    var last = Convert.ToString(rd["LastName"]);
                    fullName = $"{first} {last}".Trim();
                    role = Convert.ToString(rd["Role"]) ?? "User";

                    message = "Login successful.";
                    return true;
                }
            }
        }

        // ----------------- Schema helper -----------------

        private void EnsureSchema()
        {
            try
            {
                using (var cn = Conn())
                {
                    cn.Open();

                    // Look up columns on EquipmentLogTable, add DateReturned if missing
                    var cols = cn.GetSchema("COLUMNS", new[] { null, null, "EquipmentLogTable", null });

                    bool hasDateReturned = false;
                    foreach (DataRow r in cols.Rows)
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
                // quiet: don't crash the app if the check fails
            }
        }

        private OleDbConnection Conn() => new OleDbConnection(_cs);

        // ----------------- Debug helpers -----------------

        public void DebugDump()
        {
            var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory") as string
                          ?? AppDomain.CurrentDomain.BaseDirectory;

            using (var cn = new OleDbConnection(_cs))
            {
                cn.Open();

                var tbls = cn.GetSchema("Tables");
                var names = tbls.Rows
                    .Cast<DataRow>()
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

                MessageBox.Show(msg, "DB Debug");
            }
        }

        // ----------------- Queries used elsewhere -----------------

        public DataTable GetEmployees()
        {
            var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory") as string
                          ?? AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = Path.Combine(dataDir, "CEIS400Team5DB.accdb");

            if (!File.Exists(dbPath))
                MessageBox.Show($"DB NOT FOUND:\n{dbPath}\n\nConnStr:\n{_cs}");

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

        // === Admin helpers for Employees =========================================

        public DataTable GetEmployeesForAdmin()
        {
            const string sql = @"
        SELECT EmployeeID, [Password], FirstName, LastName, SkillLevel, Role, JobStatus
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

        // Getting Employee by ID
        public DataRow GetEmployeeById(string employeeId)
        {
            const string sql = @"
        SELECT EmployeeID, [Password], FirstName, LastName, SkillLevel, Role, JobStatus
        FROM [Employees Table]
        WHERE EmployeeID = ?";

            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("@p1", OleDbType.VarChar).Value = employeeId;
                cn.Open();

                using (var da = new OleDbDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }

        // Create a random ID like "ab1234" and ensure it doesn't already exist
        public string GenerateEmployeeId(string firstName, string lastName, int digits = 4)
        {
            // build the 2-letter prefix from initials (lowercase)
            char fi = string.IsNullOrWhiteSpace(firstName) ? 'x' : char.ToLower(firstName.Trim()[0]);
            char li = string.IsNullOrWhiteSpace(lastName) ? 'x' : char.ToLower(lastName.Trim()[0]);
            string prefix = new string(new[] { fi, li });

            using (var cn = Conn())
            {
                cn.Open();

                // try a bunch of random suffixes until we find an unused one
                for (int attempt = 0; attempt < 500; attempt++)
                {
                    string candidate = prefix + RandomDigits(digits);

                    using (var cmd = new OleDbCommand(
                        "SELECT COUNT(*) FROM [Employees Table] WHERE EmployeeID = ?", cn))
                    {
                        cmd.Parameters.Add("@p1", OleDbType.VarChar).Value = candidate;
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 0) return candidate;  // unique
                    }
                }
            }

            throw new Exception("Could not generate a unique Employee ID after many attempts.");
        }

        private static string RandomDigits(int length)
        {
            var bytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(bytes);

            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                sb.Append((bytes[i] % 10).ToString()); // 0..9

            return sb.ToString();
        }



        // Setting Role and Status

        public int SetEmployeeRole(string employeeId, string roleOrNull)
        {
            const string sql = @"UPDATE [Employees Table] SET [Role] = ? WHERE EmployeeID = ?";
            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                // 1) Role value (allow null)
                var pRole = cmd.Parameters.Add("@p1", OleDbType.VarChar);
                pRole.Value = (object)roleOrNull ?? DBNull.Value;

                // 2) EmployeeID
                cmd.Parameters.Add("@p2", OleDbType.VarChar).Value = employeeId;

                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int SetEmployeeStatus(string employeeId, string status)  // unchanged, shown for completeness
        {
            const string sql = @"UPDATE [Employees Table] SET [JobStatus] = ? WHERE EmployeeID = ?";
            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("@p1", OleDbType.VarChar).Value = status;
                cmd.Parameters.Add("@p2", OleDbType.VarChar).Value = employeeId;

                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Restore from Termination
        public int RestoreEmployee(string employeeId)
        {
            const string sql = @"
        UPDATE [Employees Table]
           SET [JobStatus] = ?, [Role] = ?
         WHERE EmployeeID = ?";

            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("@p1", OleDbType.VarChar).Value = "Active";
                cmd.Parameters.Add("@p2", OleDbType.VarChar).Value = "User";
                cmd.Parameters.Add("@p3", OleDbType.VarChar).Value = employeeId;

                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }


        public int UpdateEmployeeCore(string employeeId, int skillLevel, string role, string jobStatus)
        {
            const string sql = @"
        UPDATE [Employees Table]
           SET SkillLevel = ?, Role = ?, JobStatus = ?
         WHERE EmployeeID = ?";
            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("@p1", OleDbType.Integer).Value = skillLevel;
                cmd.Parameters.Add("@p2", OleDbType.VarChar).Value = role;
                cmd.Parameters.Add("@p3", OleDbType.VarChar).Value = jobStatus;
                cmd.Parameters.Add("@p4", OleDbType.VarChar).Value = employeeId;
                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int AddEmployee(string employeeId, string password, string first, string last, int skillLevel, string role, string jobStatus)
        {
            const string sql = @"
        INSERT INTO [Employees Table] (EmployeeID, [Password], FirstName, LastName, SkillLevel, Role, JobStatus)
        VALUES (?,?,?,?,?,?,?)";
            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("@p1", OleDbType.VarChar).Value = employeeId;
                cmd.Parameters.Add("@p2", OleDbType.VarChar).Value = password;     // store as-is per your current table
                cmd.Parameters.Add("@p3", OleDbType.VarChar).Value = first;
                cmd.Parameters.Add("@p4", OleDbType.VarChar).Value = last;
                cmd.Parameters.Add("@p5", OleDbType.Integer).Value = skillLevel;
                cmd.Parameters.Add("@p6", OleDbType.VarChar).Value = role;
                cmd.Parameters.Add("@p7", OleDbType.VarChar).Value = jobStatus;

                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int ResetEmployeePassword(string employeeId, string newPassword)
        {
            const string sql = @"UPDATE [Employees Table] SET [Password] = ? WHERE EmployeeID = ?";
            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("@p1", OleDbType.VarChar).Value = newPassword;
                cmd.Parameters.Add("@p2", OleDbType.VarChar).Value = employeeId;
                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }


        public DataTable GetEmployeeActiveCheckouts(string employeeId)
        {
            const string sql = @"
                SELECT  l.LogID,
                        l.EquipmentID,
                        e.[EquipmentName] AS Equipment,
                        l.DateCheckedOut
                FROM EquipmentLogTable AS l
                INNER JOIN [Equipment Table] AS e ON e.EquipmentID = l.EquipmentID
                WHERE l.EmployeeID = ? AND l.DateReturned IS NULL
                ORDER BY l.DateCheckedOut DESC;";

            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("p1", OleDbType.VarChar).Value = employeeId;

                using (var da = new OleDbDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable GetActiveCheckouts()
        {
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
            const string sql = @"
                INSERT INTO EquipmentLogTable (EmployeeID, EquipmentID, DateCheckedOut)
                VALUES (?,?,?)";

            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("@p1", OleDbType.VarChar).Value = employeeId;
                cmd.Parameters.Add("@p2", OleDbType.Integer).Value = equipmentId;
                cmd.Parameters.Add("@p3", OleDbType.Date).Value = when;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int Return(int logId, DateTime when, bool isDamaged, bool isLost)
        {
            const string sql = @"
                UPDATE EquipmentLogTable
                   SET DateReturned = ?, IsDamaged = ?, IsLost = ?
                 WHERE LogID = ? AND DateReturned IS NULL";

            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("@p1", OleDbType.Date).Value = when;
                cmd.Parameters.Add("@p2", OleDbType.Boolean).Value = isDamaged;
                cmd.Parameters.Add("@p3", OleDbType.Boolean).Value = isLost;
                cmd.Parameters.Add("@p4", OleDbType.Integer).Value = logId;

                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // =====================================================================
        //  Inventory CRUD (used by ManageInventoryForm)
        // =====================================================================

        public DataTable GetAllEquipment()
        {
            const string sql = @"
                SELECT EquipmentID, [EquipmentName], [Description], [Condition], [Price], [MinSkillLevel]
                FROM [Equipment Table]
                ORDER BY [EquipmentName]";
            using (var cn = Conn())
            using (var da = new OleDbDataAdapter(sql, cn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Returns one row per distinct tool name so the UI can auto-fill fields
        // when a name is selected. Access aggregates: FIRST() picks an arbitrary
        // description example; MIN() gives a conservative min skill.
        public DataTable GetEquipmentTemplates()
        {
            const string sql = @"
        SELECT  [EquipmentName]            AS [Name],
                FIRST([Description])       AS [Description],
                MIN([MinSkillLevel])       AS [MinSkillLevel]
        FROM [Equipment Table]
        GROUP BY [EquipmentName]
        ORDER BY [EquipmentName];";

            using (var cn = Conn())
            using (var da = new OleDbDataAdapter(sql, cn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


        // Helper: pass DBNull for empty strings
        private static object DbOrNull(string s) =>
            string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s;

        // Generate a new unique EquipmentID (since the column is not AutoNumber)
        private int GetNextEquipmentId(OleDbConnection cn, OleDbTransaction tx)
        {
            using (var cmdMax = new OleDbCommand(
                "SELECT MAX(EquipmentID) FROM [Equipment Table]", cn, tx))
            {
                object o = cmdMax.ExecuteScalar();
                int next = (o == null || o == DBNull.Value) ? 1 : Convert.ToInt32(o) + 1;

                using (var cmdExists = new OleDbCommand(
                    "SELECT COUNT(*) FROM [Equipment Table] WHERE EquipmentID = ?", cn, tx))
                {
                    cmdExists.Parameters.Add("@p1", OleDbType.Integer);

                    // bump until free (extremely unlikely to loop more than once)
                    while (true)
                    {
                        cmdExists.Parameters[0].Value = next;
                        int count = Convert.ToInt32(cmdExists.ExecuteScalar());
                        if (count == 0) return next;
                        next++;
                    }
                }
            }
        }

        // INSERT with generated EquipmentID
        public int AddEquipment(string name, string description, string condition, decimal price, int minSkill)
        {
            const string SQL =
                @"INSERT INTO [Equipment Table]
                        (EquipmentID, [EquipmentName], [Description], [Condition], [Price], [MinSkillLevel])
                  VALUES (?,?,?,?,?,?)";

            using (var cn = Conn())
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        int newId = GetNextEquipmentId(cn, tx);

                        using (var cmd = new OleDbCommand(SQL, cn, tx))
                        {
                            cmd.Parameters.Add("@p0", OleDbType.Integer).Value = newId;                 // We supply the ID
                            cmd.Parameters.Add("@p1", OleDbType.VarChar).Value = name;
                            cmd.Parameters.Add("@p2", OleDbType.VarChar).Value = DbOrNull(description);
                            cmd.Parameters.Add("@p3", OleDbType.VarChar).Value = DbOrNull(condition);
                            cmd.Parameters.Add("@p4", OleDbType.Currency).Value = price;
                            cmd.Parameters.Add("@p5", OleDbType.Integer).Value = minSkill;

                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        return newId;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public int UpdateEquipment(int id, string name, string description, string condition, decimal price, int minSkill)
        {
            const string sql = @"
                UPDATE [Equipment Table]
                   SET [EquipmentName] = ?,
                       [Description]   = ?,
                       [Condition]     = ?,
                       [Price]         = ?,
                       [MinSkillLevel] = ?
                 WHERE EquipmentID     = ?";
            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("@p1", OleDbType.VarChar).Value = name;
                cmd.Parameters.Add("@p2", OleDbType.VarChar).Value = DbOrNull(description);
                cmd.Parameters.Add("@p3", OleDbType.VarChar).Value = DbOrNull(condition);
                cmd.Parameters.Add("@p4", OleDbType.Currency).Value = price;
                cmd.Parameters.Add("@p5", OleDbType.Integer).Value = minSkill;
                cmd.Parameters.Add("@p6", OleDbType.Integer).Value = id;

                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteEquipment(int id)
        {
            const string sql = @"DELETE FROM [Equipment Table] WHERE EquipmentID = ?";
            using (var cn = Conn())
            using (var cmd = new OleDbCommand(sql, cn))
            {
                cmd.Parameters.Add("@p1", OleDbType.Integer).Value = id;
                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
