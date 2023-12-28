using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Player _player;

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
        private bool _isAcceling;

        #region Unity Methods
        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            _player = GetComponent<Player>();
            _speed = normalSpeed;
            hitBox2.SetActive(false);
        }

        private void Update() {
            Accelerate();
        }

        private void FixedUpdate() {
            Movement();
        }
        #endregion

        private void Movement() {
            var direction= new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            rb.velocity = direction * _speed;
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
                _player.ReduceFuel(5.8f);
            }

            if(_playerFuel.Value <= 0) {
                _speed = normalSpeed;

                _isAcceling = false;
            }
        }
    }
}
