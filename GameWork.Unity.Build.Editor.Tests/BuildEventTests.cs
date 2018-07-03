using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace GameWork.Unity.Editor.Build.Tests
{
    [TestFixture]
    public static class BuildEventTests
    {
        public static List<int> _eventsOrder = new List<int>();

        public static List<string> _eventTargets = new List<string>();

        [Test]
        public static void TestEventOrder()
        {
            _eventsOrder.Clear();

            Builder.Build();

            var expectedOrder = Enumerable.Range(1, 6).ToArray();

            Assert.True(_eventsOrder.SequenceEqual(expectedOrder),
                "Events not in correct order. \n"                
                 + "Expected: \"" + string.Join(", ", expectedOrder.Select(i => i.ToString()).ToArray()) + "\"\n" 
                 + "Got: \"" + string.Join(", ", _eventsOrder.Select(i => i.ToString()).ToArray()) + "\"\n");
        }

        [TestCase(BuildTarget.Android, new string[] { "android_pre", "mobile_pre", "android_post", "mobile_post" })]
        [TestCase(BuildTarget.iOS, new string[] { "ios_pre", "mobile_pre", "ios_post", "mobile_post" })]
        public static void TestEventTargets(BuildTarget buildTarget, string[] expected)
        {
            _eventTargets.Clear();
            Builder.Build(buildTarget);
            Assert.True(_eventTargets.SequenceEqual(expected),
                "Expected: \"" + string.Join(", ", expected) + "\"\n"
                + "Got: \"" + string.Join(", ", _eventTargets.ToArray()) + "\"");
        }

        #region TestEventOrder
        [BuildEvent(EventType.Pre, 1)]
        private static void PreBuildEvent1()
        {
            _eventsOrder.Add(1);
        }

        [BuildEvent(EventType.Pre, 2)]
        private static void PreBuildEvent2()
        {
            _eventsOrder.Add(2);
        }

        [BuildEvent(EventType.Pre, 3)]
        private static void PreBuildEvent3()
        {
            _eventsOrder.Add(3);
        }

        [BuildEvent(EventType.Post, 1)]
        private static void PostBuildEvent1()
        {
            _eventsOrder.Add(4);
        }

        [BuildEvent(EventType.Post, 2)]
        private static void PostBuildEvent2()
        {
            _eventsOrder.Add(5);
        }

        [BuildEvent(EventType.Post, 3)]
        private static void PostBuildEvent3()
        {
            _eventsOrder.Add(6);
        }
        #endregion

        #region TestEventTargets
        [BuildEvent(EventType.Pre, 100, BuildTarget.Android, BuildTarget.iOS)]
        private static void PreBuildEventMobile()
        {
            _eventTargets.Add("mobile_pre");
        }

        [BuildEvent(EventType.Post, 100, BuildTarget.Android, BuildTarget.iOS)]
        private static void PostBuildEventMobile()
        {
            _eventTargets.Add("mobile_post");
        }

        [BuildEvent(EventType.Pre, 0, BuildTarget.Android)]
        private static void PreBuildEventAndriod()
        {
            _eventTargets.Add("android_pre");
        }

        [BuildEvent(EventType.Post, 0, BuildTarget.Android)]
        private static void PostBuildEventAndroid()
        {
            _eventTargets.Add("android_post");
        }

        [BuildEvent(EventType.Pre, 0, BuildTarget.iOS)]
        private static void PreBuildEventIOS()
        {
            _eventTargets.Add("ios_pre");
        }

        [BuildEvent(EventType.Post, 0, BuildTarget.iOS)]
        private static void PostBuildEventIOS()
        {
            _eventTargets.Add("ios_post");
        }
        #endregion
    }
}