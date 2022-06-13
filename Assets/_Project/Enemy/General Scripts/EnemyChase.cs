using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyChase : MonoBehaviour
{
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
        var direction = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
    }
}