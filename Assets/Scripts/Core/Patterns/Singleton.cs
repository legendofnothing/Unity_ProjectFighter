using Core.Logging;
using UnityEngine;

namespace Core.Patterns {
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        private static T      _instance;
        private static object _lock = new object();
        private static bool isQuitting = false;

        /// <summary>
        /// Returns the singleton instance.
        /// </summary>
        public static T Instance {
            get {
                if (isQuitting) {
                    return null;
                }
                
                lock (_lock) {
                    if (_instance == null) {
                        var instances = FindObjectsOfType<T>();
                        if (instances.Length > 1) {
                            NCLogger.Log("More than one instance of singleton found in scene!", LogLevel.WARNING);
                        }

                        if (instances.Length > 0) {
                            _instance = instances[0];
                        }
                    }

                    if (_instance == null) {
                        NCLogger.Log($"No instances found, creating new {typeof(T)} instance.");
                        _instance = new GameObject($"{typeof(T)} (singleton)").AddComponent<T>();
                    }
                    
                    return _instance; 
                }
            }
        }
    
        protected void OnApplicationQuit() {
            isQuitting = true;
        }
    }
}