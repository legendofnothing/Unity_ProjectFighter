using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretBehaviour : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    private float _attackTimer;
    private float _numOfBullets;
    private bool _canAttack = true;

    public float attackRate;
    public float bulletSpeed;
    public Transform[] shootPoints;
    public GameObject enemyBullet;

    [Space]
    public float maxBulletPerCycle;
    public float delayBetweenCycle;

    #region Unity Methods
    void Start() {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }
 
    void Update() {
        Look();
        Attack();
    }   
    #endregion

    private void Look() {

        if (player != null) {
            var direction = (player.transform.position - transform.position).normalized;

            //Rotate them to look at player
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Calculate angle by getting atan of direction.x and y and convert from Rad to Degree
            rb.rotation = angle;
        }
    }

    private void Attack() {
        //Shoot bullets per attackRate
        if (Time.time > _attackTimer && _canAttack) {
            _attackTimer = Time.time + attackRate;

            for (int i = 0; i < shootPoints.Length; i++) {
                GameObject bulletInstance = Instantiate(enemyBullet, shootPoints[i].position, shootPoints[i].rotation);
                bulletInstance.GetComponent<Rigidbody2D>().AddForce(shootPoints[i].transform.up * bulletSpeed, ForceMode2D.Impulse);
            }

            _numOfBullets++;

            if (_numOfBullets >= maxBulletPerCycle) {
                StartCoroutine(CycleReset(delayBetweenCycle));
            }
        }
    }

    IEnumerator CycleReset(float delay) {
        _canAttack = false;
        _numOfBullets = 0;

        yield return new WaitForSeconds(delay);

        _canAttack = true;
    }
}