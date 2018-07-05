using System;
using UnityEditor;

namespace GameWork.Unity.Editor.Build
{
    /// <summary>
    /// Pre/Post build event attribute to decorate Pre/Post build static methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BuildEvent : Attribute
    {
        public EventType EventType { get; set; }
        public int Order { get; set; }
        public BuildTarget[] BuildTargets { get; set; }

		public BuildEvent(EventType eventType, params BuildTarget[] buildTargets) : this(eventType, 0, buildTargets)
		{
		}

		public BuildEvent(EventType eventType, int order = 0, params BuildTarget[] buildTargets)
        {
            EventType = eventType;
            Order = order;
            BuildTargets = buildTargets;
        }
    }
}