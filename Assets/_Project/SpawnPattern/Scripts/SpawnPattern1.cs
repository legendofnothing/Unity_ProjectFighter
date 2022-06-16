using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern1 : MonoBehaviour
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

    IEnumerator Sequence() {
        SetSpawner(0);
        SetSpawner(1);
        SetSpawner(2);
        SetSpawner(3);
        SetSpawner(4);
        SetSpawner(5);

        yield return new WaitForSeconds(5f);

        SetSpawner(6);
        SetSpawner(7);

        yield return new WaitForSeconds(10f);

        SetSpawner(8);
        SetSpawner(9);
        SetSpawner(10);
        SetSpawner(11);

        yield return new WaitForSeconds(15f);

        SetSpawner(12);
        SetSpawner(13);
        SetSpawner(14);
    }

    private void SetSpawner(int index) {
        for (int i = 0; i < spawners.Length; i++) {
            if (i == index) {
                spawners[i].SetActive(true);
            }
        }
    }
}
