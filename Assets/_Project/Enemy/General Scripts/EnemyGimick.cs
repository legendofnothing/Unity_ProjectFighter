using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGimick : MonoBehaviour
{
    /*  SUMMARY
    * 
    *  Misc Gimmicks for Enemy, made to make enemies more versatiles. Passed down from EnemySpawner
    * 
    */

    [HideInInspector] public bool _isLookAtPlayer;

    [HideInInspector] public bool _isRotateAround;
    [HideInInspector] public float _rotateSpeed;

    [HideInInspector] public bool _isMovingDown;
    [HideInInspector] public float _moveSpeed;

    private GameObject player;
    private Rigidbody2D rb;
    
    #region Unity Methods
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Player");

        if(player == null) {
            Debug.Log("This is the 2616th time you forgot to add the Player in, or idk case SENSITIVE!!");
        }
    }
 
    void Update() {
        if (_isLookAtPlayer) {
            Look();
        }

        if (_isRotateAround) {
            Rotate();
        }

        if (_isMovingDown) {
            MoveDownToBound();
        }
    }
    #endregion

    //Look at the player
    private void Look() {
        var direction = (player.transform.position - transform.position).normalized;

        //Rotate them to look at player
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Calculate angle by getting atan of direction.x and y and convert from Rad to Degree
        rb.rotation = angle;
    }

    //Rotate around itself _rotateSpeed / s
    private void Rotate() {
        transform.Rotate(new Vector3(0, 0, _rotateSpeed) * Time.deltaTime);
    }

    private void MoveDownToBound() {
        rb.velocity = Vector2.down * _moveSpeed;
    }
}