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
    [Header("Spawner Configs")]
    public GameObject enemyToSpawn;
    public float minSpawnRate;
    public float maxSpawnRate;

    [Header("Enemy Spawned Properties")]
    [Header("Enemy Gimmicks")]
    [Space]
    public bool isLookAtPlayer;
    [Space]
    public bool isRotateAround;
    public float rotateSpeed;
    [Space]
    public bool isMovingDown;
    public float moveSpeed;

    [Header("Enemy Attacks")]
    public bool isAttacking;
    public bool doesBulletTrackPlayer;
    [Space]
    public float attackRate;
    public float bulletSpeed;
    [Space]
    public float maxBulletPerCycle;
    public float delayBetweenCycle;

    [Header("Enemy Chases")]
    public bool isChasing;
    public float chaseSpeed;
    public float timeTilDestroy;
    
    private float _spawnTimer;

    private EnemyBehaviour enemyBehaviour;

    #region Unity Methods
    void Start() {
        enemyBehaviour = enemyToSpawn.GetComponent<EnemyBehaviour>();
    }
 
    void Update() {
        var spawnRate = Random.Range(minSpawnRate, maxSpawnRate);

        if(Time.time > _spawnTimer) {
            _spawnTimer = Time.time + spawnRate;

            Instantiate(enemyToSpawn, transform.position, transform.rotation);
            PassValue();
        }
    }
    #endregion

    //Pass Values Down, Insanely Inefficent 
    void PassValue() {
        enemyBehaviour.GIMMICK_isLookAtPlayer = isLookAtPlayer;
        enemyBehaviour.GIMMICK_isRotateAround = isRotateAround;
        enemyBehaviour.GIMMICK_isMovingDown   = isMovingDown;
        enemyBehaviour.GIMMICK_rotateSpeed    = rotateSpeed;
        enemyBehaviour.GIMMICK_moveSpeed      = moveSpeed;

        enemyBehaviour.ATTACK_isAttacking           = isAttacking;
        enemyBehaviour.ATTACK_doesBulletTrackPlayer = doesBulletTrackPlayer;
        enemyBehaviour.ATTACK_attackRate            = attackRate;
        enemyBehaviour.ATTACK_maxBulletPerCycle     = maxBulletPerCycle;
        enemyBehaviour.ATTACK_delayBetweenCycle     = delayBetweenCycle;
        enemyBehaviour.ATTACK_bulletSpeed           = bulletSpeed;

        enemyBehaviour.CHASE_isChasing      = isChasing;
        enemyBehaviour.CHASE_chaseSpeed     = chaseSpeed;
        enemyBehaviour.CHASE_timeTilDestroy = timeTilDestroy;
    }
}