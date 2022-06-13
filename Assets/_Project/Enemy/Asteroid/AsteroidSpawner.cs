using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroid;

    public float speed;
    [Space]
    public float minspawnRate;
    public float maxspawnRate;
    [Space]
    public float minSpread;
    public float maxSpread;

    private float _spawnTimer;

    #region Unity Methods
    void Start() {
        
    }
 
    void Update() {
        var spawnrate = Random.Range(minspawnRate, maxspawnRate);

        if (Time.time > _spawnTimer) {
            _spawnTimer = Time.time + spawnrate;

            SpawnAsteroid();
        }
    }
    #endregion

    private void SpawnAsteroid() {
        var spread = Random.Range(minSpread, maxSpread);

        transform.eulerAngles = new Vector3(0, 0, spread);

        GameObject asteroidInstance = Instantiate(asteroid, transform.position, transform.rotation);
        asteroidInstance.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * speed, ForceMode2D.Impulse);
    }
}