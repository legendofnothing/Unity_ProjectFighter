using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour
{
    public float attackRate;
    public GameObject enemyBullet;
    public Transform player;
    public float minDistance; 
    
    private float _attackTimer;

    #region Unity Methods
    void Start() {
        
    }
 
    void Update() {
        Shoot();
    }

    #endregion

    private void Shoot() {
        var distance = Vector2.Distance(transform.position, player.position);

        if(distance <= minDistance) {
            var shootDir = (player.position - transform.position).normalized;
        }

        //Debug Stuff
        if(distance <= minDistance) {
            Debug.DrawLine(transform.position, player.position, Color.red);
        }

        else Debug.DrawLine(transform.position, player.position, Color.green);
    }

}