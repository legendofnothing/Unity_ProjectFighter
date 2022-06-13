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

    private void Update() {
        Rotate();
    }

    void FixedUpdate() {
        Chase();
    }
    #endregion

    private void Chase() {
        var direction = (player.transform.position - transform.position).normalized;

        //Rotate them to look at player
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Calculate angle by getting atan of direction.x and y and convert from Rad to Degree
        rb.rotation = angle;
    }

    private void Rotate() {
        var direction = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
    }
}