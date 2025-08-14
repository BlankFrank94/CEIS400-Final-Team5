using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace Team5_Final.Data
{
    public class DataManager
    {
        private readonly string _cs =
            ConfigurationManager.ConnectionStrings["ToolsDb"].ConnectionString;

        private OleDbConnection Conn()
        {
            return new OleDbConnection(_cs);
        }

        public DataTable GetEmployees()
        {
            const string sql = @"SELECT EmployeeID, FirstName, LastName, SkillLevel
                                 FROM Employees ORDER BY LastName, FirstName";
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
                SELECT e.EquipmentID, e.Name, e.MinSkillLevel
                FROM Equipment e
                WHERE NOT EXISTS(
                    SELECT 1 FROM EquipmentLog l
                    WHERE l.EquipmentID = e.EquipmentID AND l.DateReturned IS NULL)
                ORDER BY e.Name";
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
            const string sql = @"
                SELECT l.LogID, l.EmployeeID,
                       emp.FirstName & ' ' & emp.LastName AS Employee,
                       l.EquipmentID, eq.Name AS Equipment,
                       l.DateCheckedOut
                FROM (EquipmentLog l
                INNER JOIN Employees emp ON emp.EmployeeID = l.EmployeeID)
                INNER JOIN Equipment eq ON eq.EquipmentID = l.EquipmentID
                WHERE l.DateReturned IS NULL
                ORDER BY l.DateCheckedOut DESC";
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
            const string sql = @"SELECT SkillLevel FROM Employees WHERE EmployeeID = ?";
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
            const string sql = @"SELECT MinSkillLevel FROM Equipment WHERE EquipmentID = ?";
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
            const string sql = @"SELECT COUNT(*) FROM EquipmentLog
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
            const string sql = @"INSERT INTO EquipmentLog
                                 (EmployeeID, EquipmentID, DateCheckedOut)
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
            const string sql = @"UPDATE EquipmentLog SET DateReturned = ?
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
