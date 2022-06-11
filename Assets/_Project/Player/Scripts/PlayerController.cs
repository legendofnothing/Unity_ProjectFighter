using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerManager playerManager;

    [SerializeField] private FloatVar playerFuel;

    [Header("Player Config")]
    public float normalSpeed;
    public float accelSpeed;

    private float _speed;

    //MoveStates
    private bool _isAcceling;

    #region Unity Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();

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
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerFuel.Value > 0) {
            _speed = accelSpeed;

            _isAcceling = true; 
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            _speed = normalSpeed;

            _isAcceling = false;
        }

        if (_isAcceling) {
            playerManager.ReduceFuel(2.8f);
        }

        if(playerFuel.Value <= 0) {
            _speed = normalSpeed;

            _isAcceling = false;
        }
    }
}
