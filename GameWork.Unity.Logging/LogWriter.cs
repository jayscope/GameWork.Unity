using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWork.Core.Logging;
using GameWork.Core.Logging.PlatformAdaptors;
using UnityEngine;

namespace GameWork.Unity.Logging
{
    public class LogWriter : ILogWriter
    {
        public void WriteLine(string line)
        {
            Debug.Log(line);
        }
    }
}
