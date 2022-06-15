using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] pickups;
    public float minSpawnRate;
    public float maxSpawnRate;

    [Header("Pickup Index [0, 1, 2 - Repair Kits, Fuel, Heat]")]
    public int pickupIndex = 0;

    private float _spawnTimer;
    #region Unity Methods
    void Start() {

    }

    void Update() {
        var spawnRate = Random.Range(minSpawnRate, maxSpawnRate);

        if (Time.time > _spawnTimer) {
            _spawnTimer = Time.time + spawnRate;

            SpawnPickups(pickupIndex);
        }
    }

    #endregion

    private void SpawnPickups(int index) {
        var pickupInstance = Instantiate(pickups[index], transform.position, transform.rotation);

        pickupInstance.GetComponent<Rigidbody2D>().velocity = Vector2.down * 0.6f;
    }
}

