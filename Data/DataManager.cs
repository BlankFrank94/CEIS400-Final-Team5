using System.Collections.Generic;

namespace CEIS400_Final_Team5.Data
{
    public class DataManager
    {
        // In-memory collections so we can compile/run now
        public List<User> Users { get; } = new();
        public List<Equipment> Equipment { get; } = new();
        public List<InventoryItem> Inventory { get; } = new();
        public List<Warehouse> Warehouses { get; } = new();
        public List<CheckoutTransaction> Transactions { get; } = new();
        public List<MaterialRequest> Requests { get; } = new();
        public List<ActivityLog> Logs { get; } = new();

        public void Initialize()
        {
            // Seed a supervisor and sample tool so UI can run without blowing up
            var sup = new Supervisor { Username = "supervisor1", HashedPassword = "hash", Role = Role.Supervisor };
            Users.Add(sup);

            var wh = new Warehouse { Name = "Main WH", Location = "Building A" };
            Warehouses.Add(wh);

            var drill = new Equipment { Name = "Cordless Drill", Category = "Power Tools", SerialNumber = "DR-1001" };
            Equipment.Add(drill);

            Inventory.Add(new InventoryItem { EquipmentId = drill.Id, WarehouseId = wh.Id, Quantity = 10 });
        }
    }
}
