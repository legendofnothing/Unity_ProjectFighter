using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerManager playerManager;

    [Header("Scriptable Objects")]
    [SerializeField] private FloatVar _overheat;
    [SerializeField] private FloatVar _playerFuel;

    [Header("Player Config")]
    public float normalSpeed;
    public float accelSpeed;

    [Space]
    public float fuelTransferAmount;
    public float overheatTransferAmount;
    public float delayToNextTransfer;

    [Space]
    public GameObject hitBox1;
    public GameObject hitBox2;

    private float _speed;

    private bool _canTransfer = true;

    //MoveStates
    private bool _isAcceling;

    #region Unity Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();

        _speed = normalSpeed;
        hitBox2.SetActive(false);
    }

    private void Update() {
        Accelerate();
        Transfer();
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && _playerFuel.Value > 0) {
            _speed = accelSpeed;

            hitBox1.SetActive(false);
            hitBox2.SetActive(true);

            _isAcceling = true; 
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            _speed = normalSpeed;

            hitBox1.SetActive(true);
            hitBox2.SetActive(false);

            _isAcceling = false;
        }

        if (_isAcceling) {
            playerManager.ReduceFuel(5.8f);
        }

        if(_playerFuel.Value <= 0) {
            _speed = normalSpeed;

            _isAcceling = false;
        }
    }

    private void Transfer() {
        if (Input.GetKeyDown(KeyCode.L) && _canTransfer) {
            StartCoroutine(TransferFuelToOverheat(fuelTransferAmount, overheatTransferAmount));
        }
    }

    private IEnumerator TransferFuelToOverheat(float fuelAmount, float overheatAmount) {
        _playerFuel.Value -= fuelAmount;
        _overheat.Value   += overheatAmount;

        _canTransfer = false;

        yield return new WaitForSeconds(delayToNextTransfer);

        _canTransfer = true;
    }
}
