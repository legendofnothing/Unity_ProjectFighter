using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _speed;

    [Header("Player Config")]
    public float normalSpeed;
    public float accelSpeed;

    #region Unity Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        _speed = normalSpeed;
    }

    private void Update() {
        Accelerate();
    }

    private void FixedUpdate() {
        Movement();
    }
    #endregion

    private void Movement() {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput   = Input.GetAxisRaw("Vertical");

        var direction       = new Vector2(horizontalInput, verticalInput).normalized; //Normalized to prevent moving faster diagonally

        rb.velocity         = direction * _speed;
    }

    private void Accelerate() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            _speed = accelSpeed;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            _speed = normalSpeed;
        }
    }
}
