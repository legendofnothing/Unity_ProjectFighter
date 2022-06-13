using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShoot : MonoBehaviour
{
    [Header("Enemy Config")]
    public bool doesBulletTrackPlayer;
    public float attackRate;
    public float minDistance;
    [Space]
    public float maxBulletPerCycle;
    public float delayBetweenCycle;
    [Space]
    public Transform[] shootPoints;

    [Header("Other Configs")]
    public GameObject enemyBullet;
    public Transform player;

    private float _attackTimer;
    private float _numOfBullets;    
    private bool _canAttack = true;

    #region Unity Methods
    void Start() {
        
    }
 
    void Update() {
        Shoot();
    }

    #endregion

    private void Shoot() {
        if (!doesBulletTrackPlayer) {
            var distance = Vector2.Distance(transform.position, player.position);

            //If in range
            if (distance <= minDistance) {
                var shootDir = (player.position - transform.position).normalized;

                if (Time.time > _attackTimer && _canAttack) {
                    _attackTimer = Time.time + attackRate;

                    for (int i = 0; i < shootPoints.Length; i++) {
                        GameObject bulletInstance = Instantiate(enemyBullet, shootPoints[i].position, shootPoints[i].rotation);
                        bulletInstance.GetComponent<Rigidbody2D>().AddForce(shootDir * 2f, ForceMode2D.Impulse);
                    }

                    _numOfBullets++;
                }
            }
        }

        else {
            if (Time.time > _attackTimer && _canAttack) {
                _attackTimer = Time.time + attackRate;

                for (int i = 0; i < shootPoints.Length; i++) {
                    GameObject bulletInstance = Instantiate(enemyBullet, shootPoints[i].position, shootPoints[i].rotation);
                    bulletInstance.GetComponent<Rigidbody2D>().AddForce(transform.up * 2f, ForceMode2D.Impulse);
                }

                _numOfBullets++;
            }
        }

        if(_numOfBullets >= maxBulletPerCycle) {
            StartCoroutine(CycleReset(delayBetweenCycle));
        }
    }
    
    //Reset cycle of attack
    IEnumerator CycleReset(float delay) {
        _canAttack = false;
        _numOfBullets = 0;

        yield return new WaitForSeconds(delay);

        _canAttack = true;
    }

}