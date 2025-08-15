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

        // Handles the process of checking out a tool to an employee
        public (bool ok, string msg) Checkout(string employeeId, int equipmentId)
        {
            // If no employee ID is provided, stop and return an error
            if (string.IsNullOrWhiteSpace(employeeId))
                return (false, "Select an employee.");

            // Get the employee's skill level from the database
            var empSkill = _data.GetEmployeeSkill(employeeId);

            // Get the tool's minimum required skill level from the database
            var toolSkill = _data.GetToolMinSkill(equipmentId);

            // If the employee's skill is lower than the tool's requirement, stop and return an error
            if (empSkill < toolSkill)
                return (false, $"Employee skill {empSkill} < tool requirement {toolSkill}.");

            // If the tool is already checked out, stop and return an error
            if (_data.IsToolCheckedOut(equipmentId))
                return (false, "Tool is already checked out.");

            // If all checks pass, create a checkout record with the current date/time
            _data.Checkout(employeeId, equipmentId, DateTime.Now);
            return (true, "Checked out.");
        }

        //working return with damage and islost
        public (bool ok, string msg) Return(int logId, bool isDamaged, bool isLost)
        {
            // Call the database method to mark the item as returned
            // Pass the current date/time along with the damage and lost flags
            int rows = _data.Return(logId, DateTime.Now, isDamaged, isLost);

            // If one row was updated, the return was successful
            // Otherwise, there was no matching active checkout (maybe already returned)
            return rows == 1
                ? (true, "Returned.")
                : (false, "No active checkout found (already returned?).");
        }


        // Overload to support older code that doesn't specify damage/loss
        // Defaults both isDamaged and isLost to false
        public (bool ok, string msg) Return(int logId)
        {
            // Call the main Return method, assuming the item is not damaged and not lost
            return Return(logId, false, false);
        }



    }
}
