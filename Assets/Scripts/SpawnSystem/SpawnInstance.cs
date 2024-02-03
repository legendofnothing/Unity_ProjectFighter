using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpawnSystem {
    public class SpawnInstance : MonoBehaviour {
        [ReadOnly] public bool spawningInInterval { private set; get; }
        [ReadOnly] public GameObject spawningObject { private set; get; }
        [ReadOnly] public float spawningInterval { private set; get; }

        private bool _canCoroutineInterval;

        public void Init(bool spawningInInterval, GameObject spawningObject, float spawningInterval) {
            this.spawningObject = spawningObject;
            this.spawningInInterval = spawningInInterval;
            this.spawningInterval = spawningInterval;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            
            if (this.spawningObject == null) return;
            if (spawningInInterval) {
                _canCoroutineInterval = true;
                StartCoroutine(SpawningInterval());
            }
            else {
                Instantiate(spawningObject, transform.position, transform.rotation);
            }
        }

        private IEnumerator SpawningInterval() {
            while (_canCoroutineInterval) {
                Instantiate(spawningObject, transform.position, transform.rotation);
                yield return new WaitForSeconds(spawningInterval);
            }
        }

        private void OnDestroy() {
            _canCoroutineInterval = false;
            StopAllCoroutines();
        }
    }
}