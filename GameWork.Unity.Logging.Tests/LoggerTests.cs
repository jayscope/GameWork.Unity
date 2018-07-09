using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GameWork.Unity.Logging.Tests
{
    public class LoggerTests
    {
        private const int LogTimeoutMS = 5;

        private static readonly Dictionary<UnityEngine.LogType, Core.Logging.LogType> LogTypeMappings =
            new Dictionary<UnityEngine.LogType, Core.Logging.LogType>
            {
                {UnityEngine.LogType.Log, Core.Logging.LogType.Debug},
                {UnityEngine.LogType.Warning, Core.Logging.LogType.Warning},
                {UnityEngine.LogType.Error, Core.Logging.LogType.Fatal}
            };

        [TestCase(UnityEngine.LogType.Log, "test message", 10000)]
        public void DoesOutperformDefaultUnityLogQueueing(UnityEngine.LogType unityLogType, string message, int itterations)
        {
            // Arrange
            var coreLogType = LogTypeMappings[unityLogType];

            var stopWatch = Stopwatch.StartNew();
            for (var i = 0; i < itterations; i++)
            {
                UnityEngine.Debug.unityLogger.Log(unityLogType, message);
            }
            stopWatch.Stop();
            var defaultDuration = stopWatch.ElapsedMilliseconds;

            long threadedDuration;
            using (var threadedLogger = new AsyncLogger())
            {
                // Act
                stopWatch.Restart();
                for (var i = 0; i < itterations; i++)
                {
                    threadedLogger.Log(coreLogType, message);
                }

                stopWatch.Stop();
                threadedDuration = stopWatch.ElapsedMilliseconds;
            }

            // Assert
            Assert.Less(threadedDuration, defaultDuration);
            UnityEngine.Debug.Log($"Threaded: {threadedDuration}, Default: {defaultDuration}.");
            
        }
    }
}
