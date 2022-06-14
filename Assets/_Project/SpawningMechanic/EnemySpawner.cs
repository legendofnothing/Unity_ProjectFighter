using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    /*  SUMMARY
    * 
    *  Handles spawning enemies and passing down properties. 
    * 
    */

    //This will be a prefab with a set list of enemies, spawn enemy by setiing Indexs 
    public GameObject[] enemies;
    public float minSpawnRate;
    public float maxSpawnRate;

    [Header("Enemy Spawned Properties")]
    public int enemyToSpawn;
    [Space]
    public bool isLookAtPlayer;
    [Space]
    public bool isRotateAround;
    public float rotateSpeed;
    [Space]
    public bool isMovingDown;
    public float moveSpeed;

    private float _spawnTimer;

    #region Unity Methods
    void Start() {
        
    }
 
    void Update() {
        var spawnRate = Random.Range(minSpawnRate, maxSpawnRate);

        if(Time.time > _spawnTimer) {
            _spawnTimer = Time.time + spawnRate;

            var enemyInstance = Instantiate(enemies[enemyToSpawn], transform.position, transform.rotation);

            var enemyGimick = enemyInstance.GetComponent<EnemyGimick>();

            //Passing values to that specific enemy instance
            enemyGimick._isLookAtPlayer = isLookAtPlayer;
            enemyGimick._isRotateAround = isRotateAround;
            enemyGimick._isMovingDown   = isMovingDown;
            enemyGimick._rotateSpeed    = rotateSpeed;
            enemyGimick._moveSpeed      = moveSpeed;
        }
    }
    #endregion
}