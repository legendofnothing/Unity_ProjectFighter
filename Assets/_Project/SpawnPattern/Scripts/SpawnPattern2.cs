using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern2 : MonoBehaviour {
    private GameObject[] spawners;

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

    IEnumerator Sequence() {

    }

    private void SetSpawner(int index) {
        for (int i = 0; i < spawners.Length; i++) {
            if (i == index) {
                spawners[i].SetActive(true);
            }
        }

    }
}