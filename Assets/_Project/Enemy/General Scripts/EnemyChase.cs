using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyChase : MonoBehaviour
{
    /*  SUMMARY
    * 
    *  Chase Player
    * 
    */

    public float speed;
    public float timeTilDestroy;

    private GameObject player;
    private Rigidbody2D rb;

    #region Unity Methods
    void Start() {
        player = GameObject.Find("Player");

        if(player == null) {
            Debug.Log("Check if player exist or case sensitive");
        }

        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, timeTilDestroy);
    }

    void FixedUpdate() {
        Chase();
    }
    #endregion

    private void Chase() {
        //Calculate distance from player to Enemy
        var direction = (player.transform.position - transform.position).normalized;

        //Move to player
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
    }
}