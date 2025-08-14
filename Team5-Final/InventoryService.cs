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

        public (bool ok, string msg) Return(int logId)
        {
            _data.Return(logId, DateTime.Now);
            return (true, "Returned.");
        }
    }
}
