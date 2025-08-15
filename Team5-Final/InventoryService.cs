using System;
using System.Data;
using Team5_Final.Data;

namespace Team5_Final.Logic
{
    public class InventoryService
    {
        private readonly DataManager _data = new DataManager();

        public DataTable Employees() => _data.GetEmployees();
        public DataTable AvailableEquipment() => _data.GetAvailableEquipment();
        public DataTable ActiveCheckouts() => _data.GetActiveCheckouts();
        public DataTable EmployeeActiveCheckouts(string employeeId) => _data.GetEmployeeActiveCheckouts(employeeId);


        public (bool ok, string msg) Checkout(string employeeId, int equipmentId)
        {
            if (string.IsNullOrWhiteSpace(employeeId))
                return (false, "Select an employee.");

            var empSkill = _data.GetEmployeeSkill(employeeId);
            var toolSkill = _data.GetToolMinSkill(equipmentId);

            if (empSkill < toolSkill)
                return (false, $"Employee skill {empSkill} < tool requirement {toolSkill}.");

            if (_data.IsToolCheckedOut(equipmentId))
                return (false, "Tool is already checked out.");

            _data.Checkout(employeeId, equipmentId, DateTime.Now);
            return (true, "Checked out.");
        }

        //working return with damage and islost
        public (bool ok, string msg) Return(int logId, bool isDamaged, bool isLost)
        {
            int rows = _data.Return(logId, DateTime.Now, isDamaged, isLost);
            return rows == 1
                ? (true, "Returned.")
                : (false, "No active checkout found (already returned?).");
        }


        // Keep the old call site working: default to not damaged
        public (bool ok, string msg) Return(int logId)
        {
            return Return(logId, false, false);
        }



    }
}
