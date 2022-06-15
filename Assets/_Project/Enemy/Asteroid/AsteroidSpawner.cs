using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
    /*  SUMMARY
     * 
     *  Spawner for Asteroids 
     * 
     */

    public GameObject asteroid;

    public float speed;
    [Space]
    public float minspawnRate;
    public float maxspawnRate;
    [Space]
    public float minSpread;
    public float maxSpread;
    [Space]
    public Sprite[] asteroidVariants;
    private float _spawnTimer;

    #region Unity Methods
    void Start() {
        
    }
 
    void Update() {
        var spawnrate = Random.Range(minspawnRate, maxspawnRate);

        //Spawns 1 Asteroid per spawnrate
        if (Time.time > _spawnTimer) {
            _spawnTimer = Time.time + spawnrate;

            SpawnAsteroid();
        }
    }
    #endregion

    private void SpawnAsteroid() {
        //Spawn Spread
        var spread = Random.Range(minSpread, maxSpread);

        //Rotate the Point to match the spread angle 
        transform.eulerAngles = new Vector3(0, 0, spread);

        //Instanitate and AddForce to spanwed Asteroid
        GameObject asteroidInstance = Instantiate(asteroid, transform.position, transform.rotation);
        asteroidInstance.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * speed, ForceMode2D.Impulse);
    }
}