using System;
using System.Linq;
using CEIS400_Final_Team5.Data;

namespace CEIS400_Final_Team5.Logic
{
    public class CheckoutService
    {
        private readonly DataManager _data;
        public CheckoutService(DataManager data) => _data = data;

        public Guid Create(Guid userId, Guid equipmentId, DateTime? dueAt = null)
        {
            var tx = new CheckoutTransaction
            {
                UserId = userId,
                EquipmentId = equipmentId,
                DueAt = dueAt,
                Status = TransactionStatus.Open
            };
            _data.Transactions.Add(tx);
            // TODO: set equipment status, decrement inventory, log action
            return tx.Id;
        }

        public void Complete(Guid transactionId)
        {
            var tx = _data.Transactions.FirstOrDefault(t => t.Id == transactionId);
            if (tx == null) return;
            tx.ReturnedAt = DateTime.UtcNow;
            tx.Status = TransactionStatus.Completed;
            // TODO: increment inventory, set equipment available, log action
        }

        public TransactionStatus GetStatus(Guid transactionId)
        {
            var tx = _data.Transactions.FirstOrDefault(t => t.Id == transactionId);
            return tx?.Status ?? TransactionStatus.Cancelled;
        }
    }
}
