using System.Collections;
using Core.Patterns;
using UnityEngine;

namespace Player {
    public class Player : Singleton<Player> {
        private PlayerAttack playerAttack;
        private Animator anim;
        
        [Header("Scriptable Objects")]
        [SerializeField] private float _playerHP;
        [SerializeField] private float _playerFuel;
        [SerializeField] private float _score;

        [Header("Player Configs")]
        public float playerHP;
        public float playerFuel;
        public AnimationClip Hit;
        public GameObject damagedSprite;

        private float _timer = 0f;
        [HideInInspector] public bool _canDamage = true;
        [HideInInspector] public bool _hasDied;

        #region Unity Methods
        private void Awake() {
            _playerHP = playerHP;
            _playerFuel = playerFuel;
            _score = 0;
        }

        private void Start() {
            playerAttack = GetComponent<PlayerAttack>();
            anim = GetComponent<Animator>();
            damagedSprite.SetActive(false);
        }

        private void Update() {
            //Making sure these values doesnt go to infinity and beyond
            if(_playerHP <= 0) {
                _playerHP = -1;
                _canDamage = false;

                var isDone = true;

                if (isDone) {
                    anim.SetBool("Die", true);
                    gameObject.GetComponent<PlayerController>().enabled = false;
                    gameObject.GetComponent<PlayerAttack>().enabled = false;
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    _hasDied = true;
                    isDone = false;
                }
            }

            if (_playerFuel <= 0) {
                _playerFuel = -1;
            }

            if (_playerHP > playerHP) {
                _playerHP = playerHP;
            }

            if (_playerFuel > playerFuel) {
                _playerFuel = playerFuel;
            }

            playerAttack._canShoot = _canDamage;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Destroy")) {
                Destroy(gameObject);
            }   
        }
        #endregion

        //Reduce Fuel by amount/1s
        public void ReduceFuel(float amount) {
            if(_timer < Time.time) {
                _timer = Time.time + 1f;

                _playerFuel -= amount;
            }
        }

        public void TakeDamage(float amount) {
            if (_canDamage) {
                _playerHP -= amount;
                StartCoroutine(IFrames());
            }
        }

        public void AddScore(float amount) {
            _score += amount;
        }

        //Invincible Frames
        IEnumerator IFrames() {
            _canDamage = false;
            anim.SetTrigger("Hit");
            damagedSprite.SetActive(true);

            yield return new WaitForSeconds(Hit.length);

            damagedSprite.SetActive(false);

            _canDamage = true;
        }
    }
}