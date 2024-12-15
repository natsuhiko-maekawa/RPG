using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Utility
{
    public static class MyDebug
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(object message)
        {
            Debug.Log(message);
        }

        [Conditional("UNITY_EDITOR")]
        public static void LogAssertion(object message)
        {
            Debug.LogAssertion(message);
        }

        [Conditional("UNITY_EDITOR")]
        public static void LogError(object message)
        {
            Debug.LogError(message);
        }

        [Conditional("UNITY_EDITOR")]
        public static void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }

        [Conditional("UNITY_EDITOR")]
        public static void Assert(bool condition)
        {
            Debug.Assert(condition);
        }
    }
}