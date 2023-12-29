using System.Collections;
using Player;
using UnityEngine;

namespace Boss {
    public class TurretManager : MonoBehaviour
    {
        public float turretHP;
        public AnimationClip bossStart;
        public float scoreToAdd;

        [Space]
        //Audios
        public AudioClip enemyHitAudio;
        public AudioClip enemyDieAudio;

        private float currHP;
        private Animator anim;

        private float _damageTimer;
        private bool _isTouching;
        private bool _hasBossStart;

        private TurretBehaviour turretBehaviour;

        [SerializeField] private float bossHP;

        #region Unity Methods

        private void Start() {
            currHP = turretHP;
            anim = GetComponent<Animator>();
            turretBehaviour = GetComponent<TurretBehaviour>();

            StartCoroutine(WaitForBossStart());
        }

        private void Update() {
            if(currHP <= 0) {
                var isDone = true;

                if (isDone) {
                    anim.SetBool("Die", true);
                }
            }

            if (_damageTimer < Time.time && _isTouching && _hasBossStart) {
                _damageTimer = Time.time + 1f;

                Player.Player.Instance.TakeDamage(10f);
            }
        }

        //If player still in the enemy deal damage/s
        private void OnTriggerStay2D(Collider2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
                _isTouching = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
                _isTouching = false;
            }
        }

        #endregion

        public void TakeDamage(float dmg) {
            if (_hasBossStart) {
                currHP -= dmg;
                bossHP -= dmg;
                anim.SetTrigger("Hit");

                Player.Player.Instance.AddScore(scoreToAdd);
            }
        }

        public void Die() {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        IEnumerator WaitForBossStart() {
            turretBehaviour.enabled = false;
            yield return new WaitForSeconds(bossStart.length);
            turretBehaviour.enabled = true;
            _hasBossStart = true;
        }
    }
}