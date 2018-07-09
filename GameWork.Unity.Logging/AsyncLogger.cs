using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWork.Core.Logging.Entries;
using GameWork.Core.Logging.Formatting;

namespace GameWork.Unity.Logging
{
    public class AsyncLogger : Core.Logging.Loggers.AsyncLogger<TickLogEntry>
    {
        public AsyncLogger() : base(new LogWriter(), new TickLogFormatter(), new TickLogFactory())
        {
        }
    }
}
