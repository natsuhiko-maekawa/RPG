using UnityEngine;

namespace BattleScene.Framework.Utility
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = (T)FindAnyObjectByType(typeof(T));

                if (_instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) +
                                   " is needed in the scene, but there is none.");
                }

                return _instance;
            }
        }
    }
}