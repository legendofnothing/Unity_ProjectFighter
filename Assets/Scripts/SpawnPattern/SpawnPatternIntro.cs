using System.Collections;
using UnityEngine;

namespace SpawnPattern {
    public class SpawnPatternIntro : MonoBehaviour
    {
        public GameObject[] spawners;

        #region Unity Methods
        void Start() {
            for (int i = 0; i < spawners.Length; i++) {
                spawners[i].SetActive(false);
            }

            StartCoroutine(Sequence());
        }
 
        void Update() {
       
        }

        #endregion

        private void SetSpawner(int index) {
            for (int i = 0; i < spawners.Length; i++) {
                if(i == index) {
                    spawners[i].SetActive(true);
                }
            }
        }

        private void DeactivateSpawner(int index) {
            for (int i = 0; i < spawners.Length; i++) {
                if (i == index) {
                    spawners[i].SetActive(false);
                }
            }
        }

        IEnumerator Sequence() {
            SetSpawner(0);
            SetSpawner(1);

            yield return new WaitForSeconds(10f);

            SetSpawner(2);
            SetSpawner(3);
            SetSpawner(4);
            SetSpawner(5);
            SetSpawner(6);

            yield return new WaitForSeconds(2f);

            SetSpawner(7);
            SetSpawner(8);

            yield return new WaitForSeconds(8f);

            SetSpawner(9);
            SetSpawner(10);

            yield return new WaitForSeconds(8f);

            SetSpawner(11);
            SetSpawner(12);
            SetSpawner(13);

            yield return new WaitForSeconds(8f);
        }
    }
}