using System.Collections;
using UnityEngine;

namespace Enemy {
    public class EnemyBehaviour : MonoBehaviour
    {
        /*  SUMMARY
    * 
    *  Behaviour of Enemy, Set properties in EnemySpawner 
    * 
    */

        #region Variables
        //Other Configs
        private GameObject player;
        private Rigidbody2D rb;

        //Gimmicks
        [HideInInspector] public bool  GIMMICK_isLookAtPlayer; //Check if enemy should look at player

        [HideInInspector] public bool  GIMMICK_isRotateAround; //Check if enemy should rotate around
        [HideInInspector] public float GIMMICK_rotateSpeed;

        [HideInInspector] public bool  GIMMICK_isMovingDown;   //Check if enemy should move down to bound
        [HideInInspector] public float GIMMICK_moveSpeed;

        //Attack
        [HideInInspector] public bool  ATTACK_isAttacking;    //Check if enemy should attack

        [HideInInspector] public bool  ATTACK_doesBulletTrackPlayer;

        [HideInInspector] public float ATTACK_attackRate;

        [HideInInspector] public float ATTACK_maxBulletPerCycle;
        [HideInInspector] public float ATTACK_delayBetweenCycle;

        [HideInInspector] public float ATTACK_bulletSpeed;

        //Chasing
        [HideInInspector] public bool  CHASE_isChasing;     //Check if enemy should chase player
        [HideInInspector] public float CHASE_chaseSpeed;
        [HideInInspector] public float CHASE_timeTilDestroy;

        //Internal Attack Configs
        private float _attackTimer;
        private float _numOfBullets;
        private bool _canAttack = true;


        [SerializeField] private GameObject enemyBullet;
        [SerializeField] private Transform[] shootPoints;

        private EnemyManager enemyManager;
        #endregion

        #region Unity Methods
        void Start() {
            rb = GetComponent<Rigidbody2D>();

            player = GameObject.Find("Player");

            if (player == null) {
                Debug.Log("This is the 2616th time you forgot to add the Player in, or idk case SENSITIVE!!");
            }

            enemyManager = GetComponent<EnemyManager>();
        }

        void Update() {
            if (GIMMICK_isLookAtPlayer) {
                Look();
            }

            if (GIMMICK_isRotateAround) {
                Rotate();
            }

            if (GIMMICK_isMovingDown) {
                MoveDownToBound();
            }

            if (ATTACK_isAttacking) {
                Shoot();
            }

            if (CHASE_isChasing) {
                Chase();
            }
        }
        #endregion

        #region Gimmicks
        //Look at the player
        private void Look() {

            if(player != null) {
                var direction = (player.transform.position - transform.position).normalized;

                //Rotate them to look at player
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Calculate angle by getting atan of direction.x and y and convert from Rad to Degree
                rb.rotation = angle;
            }
        }

        //Rotate around itself _rotateSpeed / s
        private void Rotate() {
            transform.Rotate(new Vector3(0, 0, GIMMICK_rotateSpeed) * Time.deltaTime);
        }

        private void MoveDownToBound() {
            rb.velocity = Vector2.down * GIMMICK_moveSpeed;
        }
        #endregion

        #region Shooting
        private void Shoot() {
            if (player != null) {
                //Shoot with Tracking Player
                if (ATTACK_doesBulletTrackPlayer) {
                    var shootDir = (player.transform.position - transform.position).normalized;

                    //Shoot bullets per attackRate
                    if (Time.time > _attackTimer && _canAttack) {
                        _attackTimer = Time.time + ATTACK_attackRate;

                        for (int i = 0; i < shootPoints.Length; i++) {
                            GameObject bulletInstance = Instantiate(enemyBullet, shootPoints[i].position, shootPoints[i].rotation);
                            bulletInstance.GetComponent<Rigidbody2D>().AddForce(shootDir * ATTACK_bulletSpeed, ForceMode2D.Impulse);
                        }

                        _numOfBullets++;
                    }
                }

                //Shoot without Tracking Player
                else {
                    //Shoot bullets per attackRate
                    if (Time.time > _attackTimer && _canAttack) {
                        _attackTimer = Time.time + ATTACK_attackRate;

                        for (int i = 0; i < shootPoints.Length; i++) {
                            GameObject bulletInstance = Instantiate(enemyBullet, shootPoints[i].position, shootPoints[i].rotation);
                            bulletInstance.GetComponent<Rigidbody2D>().AddForce(shootPoints[i].transform.up * ATTACK_bulletSpeed, ForceMode2D.Impulse);
                        }

                        _numOfBullets++;
                    }
                }

                if (_numOfBullets >= ATTACK_maxBulletPerCycle) {
                    StartCoroutine(CycleReset(ATTACK_delayBetweenCycle));
                }
            }
        }

        //Reset cycle of attack
        IEnumerator CycleReset(float delay) {
            _canAttack = false;
            _numOfBullets = 0;

            yield return new WaitForSeconds(delay);

            _canAttack = true;
        }
        #endregion

        private void Chase() {
            if(player != null) {
                //Calculate distance from player to Enemy
                var direction = (player.transform.position - transform.position).normalized;

                //Move to player
                rb.velocity = new Vector2(direction.x, direction.y) * CHASE_chaseSpeed;

                StartCoroutine(ChaseCoroutine());
            }
        }

        IEnumerator ChaseCoroutine() {
            yield return new WaitForSeconds(CHASE_timeTilDestroy);

            enemyManager.Die();
        }
    }
}