using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;

namespace GameWork.Unity.Build.Editor.Tests
{
    public class BuildEventTests
    {
        public static List<int> EventsOrder = new List<int>();

        public static List<string> EventTargets = new List<string>();

        [Test]
        public void TestEventOrder()
        {
            EventsOrder.Clear();

            Builder.Build();

            var expectedOrder = Enumerable.Range(1, 6).ToArray();

            Assert.True(EventsOrder.SequenceEqual(expectedOrder),
                "Events not in correct order. \n"                
                 + "Expected: \"" + string.Join(", ", expectedOrder.Select(i => i.ToString()).ToArray()) + "\"\n" 
                 + "Got: \"" + string.Join(", ", EventsOrder.Select(i => i.ToString()).ToArray()) + "\"\n");
        }

        [TestCase(BuildTarget.Android, new[] { "android_pre", "mobile_pre", "android_post", "mobile_post" })]
        [TestCase(BuildTarget.iOS, new[] { "ios_pre", "mobile_pre", "ios_post", "mobile_post" })]
        public void TestEventTargets(BuildTarget buildTarget, string[] expected)
        {
            EventTargets.Clear();
            Builder.Build(buildTarget);
            Assert.True(EventTargets.SequenceEqual(expected),
                "Expected: \"" + string.Join(", ", expected) + "\"\n"
                + "Got: \"" + string.Join(", ", EventTargets.ToArray()) + "\"");
        }

        #region TestEventOrder
        [BuildEvent(EventType.Pre, 1)]
        private static void PreBuildEvent1()
        {
            EventsOrder.Add(1);
        }

        [BuildEvent(EventType.Pre, 2)]
        private static void PreBuildEvent2()
        {
            EventsOrder.Add(2);
        }

        [BuildEvent(EventType.Pre, 3)]
        private static void PreBuildEvent3()
        {
            EventsOrder.Add(3);
        }

        [BuildEvent(EventType.Post, 1)]
        private static void PostBuildEvent1()
        {
            EventsOrder.Add(4);
        }

        [BuildEvent(EventType.Post, 2)]
        private static void PostBuildEvent2()
        {
            EventsOrder.Add(5);
        }

        [BuildEvent(EventType.Post, 3)]
        private static void PostBuildEvent3()
        {
            EventsOrder.Add(6);
        }
        #endregion

        #region TestEventTargets
        [BuildEvent(EventType.Pre, 100, BuildTarget.Android, BuildTarget.iOS)]
        private static void PreBuildEventMobile()
        {
            EventTargets.Add("mobile_pre");
        }

        [BuildEvent(EventType.Post, 100, BuildTarget.Android, BuildTarget.iOS)]
        private static void PostBuildEventMobile()
        {
            EventTargets.Add("mobile_post");
        }

        [BuildEvent(EventType.Pre, 0, BuildTarget.Android)]
        private static void PreBuildEventAndriod()
        {
            EventTargets.Add("android_pre");
        }

        [BuildEvent(EventType.Post, 0, BuildTarget.Android)]
        private static void PostBuildEventAndroid()
        {
            EventTargets.Add("android_post");
        }

        [BuildEvent(EventType.Pre, 0, BuildTarget.iOS)]
        private static void PreBuildEventIOS()
        {
            EventTargets.Add("ios_pre");
        }

        [BuildEvent(EventType.Post, 0, BuildTarget.iOS)]
        private static void PostBuildEventIOS()
        {
            EventTargets.Add("ios_post");
        }
        #endregion
    }
}