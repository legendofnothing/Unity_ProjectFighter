using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    //Basically Heart of the game

    public GameObject[] spawners;
    public float[] spawnerDuration;


    private void Start() {
        StartCoroutine(LevelSequence());
    }

    IEnumerator LevelSequence() {
        yield return new WaitForSeconds(spawnerDuration[0]);
        var spawner1 = Instantiate(spawners[0], transform.position, transform.rotation);

        yield return new WaitForSeconds(spawnerDuration[1] - 15f);
        Destroy(spawner1);

        yield return new WaitForSeconds(15f);
        var spawner2 = Instantiate(spawners[1], transform.position, transform.rotation);

        yield return new WaitForSeconds(spawnerDuration[2] - 15f);
        Destroy(spawner2);

        yield return new WaitForSeconds(15f);
        var spawner3 = Instantiate(spawners[2], transform.position, transform.rotation);

        yield return new WaitForSeconds(spawnerDuration[3] - 15f);
        Destroy(spawner3);

        yield return new WaitForSeconds(15f);
        var spawner4 = Instantiate(spawners[3], transform.position, transform.rotation);
    }
}
