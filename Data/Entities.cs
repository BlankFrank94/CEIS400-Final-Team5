namespace CEIS400_Final_Team5.Data
{
    public enum Role { Employee, Supervisor }
    public enum EquipmentStatus { Available, CheckedOut, Lost, Damaged }
    public enum RequestStatus { Submitted, Approved, Denied, Fulfilled, Cancelled }
    public enum TransactionStatus { Open, Completed, Overdue, Cancelled }

    public abstract class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public Role Role { get; set; }
    }

    public class Employee : User
    {
        public string Department { get; set; } = string.Empty;
    }

    public class Supervisor : User
    {
        public string Title { get; set; } = "Supervisor";
    }

    public class Equipment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;
    }

    public class Warehouse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }

    public class InventoryItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EquipmentId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Quantity { get; set; }
        // Skeleton behavior
        public bool IsLowStock(int threshold) => Quantity <= threshold;
    }

    public class CheckoutTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid EquipmentId { get; set; }
        public DateTime CheckedOutAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Open;
    }

    public class MaterialRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequesterId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Submitted;
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }

    public class ActivityLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public Guid? UserId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }

    public class Report
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "Placeholder Report";
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public string Body { get; set; } = string.Empty; // Could be CSV/JSON later
    }
}
