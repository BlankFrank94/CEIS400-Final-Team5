using System;
using System.Linq;
using CEIS400_Final_Team5.Data;

namespace CEIS400_Final_Team5.Logic
{
    public class RequestService
    {
        private readonly DataManager _data;
        public RequestService(DataManager data) => _data = data;

        public Guid SubmitRequest(Guid requesterId, string itemName, int quantity)
        {
            var req = new MaterialRequest
            {
                RequesterId = requesterId,
                ItemName = itemName,
                Quantity = quantity,
                Status = RequestStatus.Submitted
            };
            _data.Requests.Add(req);
            return req.Id;
        }

        public RequestStatus CheckStatus(Guid requestId)
        {
            var req = _data.Requests.FirstOrDefault(r => r.Id == requestId);
            return req?.Status ?? RequestStatus.Cancelled;
        }
    }
}
