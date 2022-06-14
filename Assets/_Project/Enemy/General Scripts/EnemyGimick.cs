using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGimick : MonoBehaviour
{
    /*  SUMMARY
    * 
    *  Misc Gimmicks for Enemy, made to make enemies more versatiles 
    * 
    */

    //Kinda like a switch on and off
    public bool LookAtPlayer;
    [Space]
    public bool RotateAtPoint;
    public float rotationPerSec;

    [Space]
    //This handles moving down to Destroy bound, creating illusion of a SHUMP game 
    public bool MoveDown;
    public float speed;

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
        if (LookAtPlayer) {
            Look();
        }

        if (RotateAtPoint) {
            Rotate();
        }

        if (MoveDown) {
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

    //Rotate around itself on rotationPerSec
    private void Rotate() {
        transform.Rotate(new Vector3(0, 0, rotationPerSec) * Time.deltaTime);
    }

    private void MoveDownToBound() {
        rb.velocity = Vector2.down * speed;
    }
}