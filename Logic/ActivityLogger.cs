using System;
using System.Collections.Generic;
using System.Linq;
using CEIS400_Final_Team5.Data;

namespace CEIS400_Final_Team5.Logic
{
    public class ActivityLogger
    {
        private readonly DataManager _data;
        public ActivityLogger(DataManager data) => _data = data;

        public Guid RecordAction(Guid? userId, string action, string details = "")
        {
            var log = new ActivityLog { UserId = userId, Action = action, Details = details };
            _data.Logs.Add(log);
            return log.Id;
        }

        public IEnumerable<ActivityLog> GetLogsByUser(Guid userId)
            => _data.Logs.Where(l => l.UserId == userId);
    }
}
