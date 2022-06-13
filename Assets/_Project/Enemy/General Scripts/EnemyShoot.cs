using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShoot : MonoBehaviour
{
    [Header("Enemy Config")]
    public float attackRate;
    public float minDistance;
    public float maxBulletPerCycle;
    public float delayBetweenCycle;

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
        var distance = Vector2.Distance(transform.position, player.position);

        //If in range
        if(distance <= minDistance) {
            var shootDir = (player.position - transform.position).normalized;

            if(Time.time > _attackTimer && _canAttack) {
                _attackTimer = Time.time + attackRate;

                GameObject bulletInstance = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                bulletInstance.GetComponent<Rigidbody2D>().AddForce(shootDir * 2f, ForceMode2D.Impulse);

                _numOfBullets++;
            }
        }

        if(_numOfBullets >= maxBulletPerCycle) {
            StartCoroutine(CycleReset(delayBetweenCycle));
        }

        //Debug Stuff
        if(distance <= minDistance) {
            Debug.DrawLine(transform.position, player.position, Color.red);
        }

        else Debug.DrawLine(transform.position, player.position, Color.green);
    }
    
    //Reset cycle of attack
    IEnumerator CycleReset(float delay) {
        _canAttack = false;
        _numOfBullets = 0;

        yield return new WaitForSeconds(delay);

        _canAttack = true;
    }

}