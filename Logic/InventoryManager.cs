using System.Collections.Generic;
using System.Linq;
using CEIS400_Final_Team5.Data;

namespace CEIS400_Final_Team5.Logic
{
    public class InventoryManager
    {
        private readonly DataManager _data;
        public InventoryManager(DataManager data) => _data = data;

        public IEnumerable<Equipment> GetAvailableEquipment()
            => _data.Equipment.Where(e => e.Status == EquipmentStatus.Available);

        public void UpdateStock(Guid equipmentId, int delta)
        {
            var item = _data.Inventory.FirstOrDefault(i => i.EquipmentId == equipmentId);
            if (item == null) return; // TODO: add error handling
            item.Quantity += delta;
        }

        public bool IsLowStock(Guid equipmentId, int threshold)
        {
            var item = _data.Inventory.FirstOrDefault(i => i.EquipmentId == equipmentId);
            return item?.IsLowStock(threshold) ?? false;
        }
    }
}
