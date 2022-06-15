using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        DeactivateSpawner(0);
        DeactivateSpawner(1);

        SetSpawner(2);
        SetSpawner(3);
        SetSpawner(4);
        SetSpawner(5);
        SetSpawner(6);

        yield return new WaitForSeconds(2f);

        DeactivateSpawner(2);
        DeactivateSpawner(3);
        DeactivateSpawner(4);
        DeactivateSpawner(5);
        DeactivateSpawner(6);

        SetSpawner(7);
        SetSpawner(8);

        yield return new WaitForSeconds(8f);

        DeactivateSpawner(7);
        DeactivateSpawner(8);

        SetSpawner(9);
        SetSpawner(10);

        yield return new WaitForSeconds(8f);

        DeactivateSpawner(9);
        DeactivateSpawner(10);

        SetSpawner(11);
        SetSpawner(12);
        SetSpawner(13);

        yield return new WaitForSeconds(8f);

        DeactivateSpawner(11);
        DeactivateSpawner(12);
        DeactivateSpawner(13);
    }
}