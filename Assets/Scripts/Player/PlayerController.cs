using Sirenix.OdinInspector;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour {
        [TitleGroup("Refs")]
        public GameObject hitBox1;
        public GameObject hitBox2;

        private Rigidbody2D rb;
        private Player _player;
        private bool _isAccelerating;
        
        #region Unity Methods
        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            _player = GetComponent<Player>();
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
            rb.velocity = direction * _player.currentSpeed;
        }

        private void Accelerate() {
            if (Input.GetKeyDown(KeyCode.LeftShift) && _player.currentFuel > 0) {
                _player.currentSpeed = _player.stats.accelSpeed;
                hitBox1.SetActive(false);
                hitBox2.SetActive(true);
                _isAccelerating = true;
            }

            else if (Input.GetKeyUp(KeyCode.LeftShift)) {
                _player.currentSpeed = _player.stats.normalSpeed;
                hitBox1.SetActive(true);
                hitBox2.SetActive(false);
                _isAccelerating = false;
            }

            if (_isAccelerating) {
                _player.ReduceFuel(_player.stats.fuelBurnOnAccel);
                if (!(_player.currentFuel <= 0)) return;
                _player.currentFuel = 0;
                _player.currentSpeed = _player.stats.normalSpeed;
                _isAccelerating = false;
            }
        }
    }
}
