using System.Collections;
using UnityEngine;

namespace SpawnPattern {
    public class SpawnPattern3 : MonoBehaviour
    {
        public GameObject[] spawners;

        public AnimationClip bossStart;
 
        #region Unity Methods
        void Start() {
            for (int i = 1; i < spawners.Length; i++) {
                spawners[i].SetActive(false);
            }

            StartCoroutine(Sequence());
        }
 
        void Update() {
        
        }
        #endregion

        IEnumerator Sequence() {
            yield return new WaitForSeconds(bossStart.length);

            SetSpawner(1);
            SetSpawner(2);
            SetSpawner(3);
            SetSpawner(4);
            SetSpawner(5);
            SetSpawner(7);
            SetSpawner(8);
            SetSpawner(9);

            yield return new WaitForSeconds(2f);

            SetSpawner(10);
            SetSpawner(11);
            SetSpawner(12);
        }

        private void SetSpawner(int index) {
            for (int i = 0; i < spawners.Length; i++) {
                if (i == index) {
                    spawners[i].SetActive(true);
                }
            }

        }
    }
}