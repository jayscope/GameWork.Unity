using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWork.Core.Logging.Entries;
using GameWork.Core.Logging.Factories;
using GameWork.Core.Logging;

namespace GameWork.Unity.Logging
{
    public class TickLogFactory : ILogFactory<TickLogEntry>
    {
        public TickLogEntry Create(LogType logType, string message, object[] args, Exception exception = null)
        {
            return new TickLogEntry
            {
                LogType = logType,
                TimeStamp = DateTime.UtcNow,
                Message = message,
                Args = args,
                Exception = exception,
                Tick = UnityEngine.Time.frameCount
            };
        }
    }
}
