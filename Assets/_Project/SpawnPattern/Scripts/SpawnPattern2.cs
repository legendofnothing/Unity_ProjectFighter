using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern2 : MonoBehaviour {
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

    IEnumerator Sequence() {
        SetSpawner(5);
        SetSpawner(6);
        SetSpawner(7);
        SetSpawner(8);
        SetSpawner(9);
        SetSpawner(10);
        SetSpawner(11);
        SetSpawner(12);

        yield return new WaitForSeconds(8f);

        SetSpawner(0);
        SetSpawner(1);

        yield return new WaitForSeconds(35f);

        SetSpawner(2);
        SetSpawner(3);

        SetSpawner(13);
        SetSpawner(14);
        SetSpawner(15);

        yield return new WaitForSeconds(35f);

        SetSpawner(4);
    }

    private void SetSpawner(int index) {
        for (int i = 0; i < spawners.Length; i++) {
            if (i == index) {
                spawners[i].SetActive(true);
            }
        }

    }
}