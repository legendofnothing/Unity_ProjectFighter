using System.Collections;
using Core.Patterns;
using ScriptableObjects;
using UnityEngine;

namespace Player {
    public class Player : Singleton<Player> {
        private PlayerAttack playerAttack;
        private Animator anim;
        
        [Header("Scriptable Objects")]
        [SerializeField] private FloatVar _playerHP;
        [SerializeField] private FloatVar _playerFuel;
        [SerializeField] private IntVar _score;

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
            _playerHP.Value = playerHP;
            _playerFuel.Value = playerFuel;
            _score.Value = 0;
        }

        private void Start() {
            playerAttack = GetComponent<PlayerAttack>();
            anim = GetComponent<Animator>();
            damagedSprite.SetActive(false);
        }

        private void Update() {
            //Making sure these values doesnt go to infinity and beyond
            if(_playerHP.Value <= 0) {
                _playerHP.Value = -1;
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

            if (_playerFuel.Value <= 0) {
                _playerFuel.Value = -1;
            }

            if (_playerHP.Value > playerHP) {
                _playerHP.Value = playerHP;
            }

            if (_playerFuel.Value > playerFuel) {
                _playerFuel.Value = playerFuel;
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

                _playerFuel.Value -= amount;
            }
        }

        public void TakeDamage(float amount) {
            if (_canDamage) {
                _playerHP.Value -= amount;
                StartCoroutine(IFrames());
            }
        }

        public void AddScore(float amount) {
            _score.Value += amount;
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