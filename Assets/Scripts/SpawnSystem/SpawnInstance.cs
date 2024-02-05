using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpawnSystem {
    public struct SpawnInstanceSetting {
        public GameObject spawningObject;
        public float spawningDelay;
        public bool spawningInInterval;
        public float spawningInterval;
    }
    
    public class SpawnInstance : MonoBehaviour {
        private SpawnInstanceSetting _setting;
        private bool _canCoroutineInterval;

        public void Init(SpawnInstanceSetting setting) {
            _setting = setting;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            _canCoroutineInterval = true;
            
            if (_setting.spawningObject == null) return;
            StartCoroutine(StartSpawning());
        }

        private IEnumerator StartSpawning() {
            yield return new WaitForSeconds(_setting.spawningDelay);
            if (_setting.spawningInInterval) {
                StartCoroutine(SpawningInterval());
            } else {
                Instantiate(_setting.spawningObject, transform.position, transform.rotation);
            }
        }

        private IEnumerator SpawningInterval() {
            while (_canCoroutineInterval) {
                Instantiate(_setting.spawningObject, transform.position, transform.rotation);
                yield return new WaitForSeconds(_setting.spawningInterval);
            }
        }

        private void OnDestroy() {
            _canCoroutineInterval = false;
            StopAllCoroutines();
        }
    }
}