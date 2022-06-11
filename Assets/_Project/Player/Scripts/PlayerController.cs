using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Player Config")]
    public float speed;

    #region Unity Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        
    }

    private void FixedUpdate() {
        Movement();
    }
    #endregion

    private void Movement() {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput   = Input.GetAxisRaw("Vertical");

        var direction       = new Vector2(horizontalInput, verticalInput).normalized;

        rb.velocity         = direction * speed;
    }
}
