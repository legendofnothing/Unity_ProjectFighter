using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGimick : MonoBehaviour
{
    //Kinda like a switch on and off
    public bool LookAtPlayer;
    [Space]
    public bool RotateAtPoint;
    public float rotationPerSec;

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
    }
    #endregion

    private void Look() {
        var direction = (player.transform.position - transform.position).normalized;

        //Rotate them to look at player
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Calculate angle by getting atan of direction.x and y and convert from Rad to Degree
        rb.rotation = angle;
    }

    private void Rotate() {
        transform.Rotate(new Vector3(0, 0, rotationPerSec) * Time.deltaTime);
    }
}