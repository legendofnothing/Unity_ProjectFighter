using Asteroid;
using Boss;
using Enemy;
using Player;
using UnityEngine;

namespace Bullet {
    public class BulletBehaviour : MonoBehaviour
    {
        /* 
     * SUMMARY
     * 
     * Destroy bullet upon hitting certain objects, dealing damage where necessary
     * 
     */
        public float timeTilDestroy;

        private float _damage;

        private GameObject player;
        private PlayerAttack playerAttack;

        private void Start() {
            player = GameObject.Find("Player");
            playerAttack = player.GetComponent<PlayerAttack>();

            if(player == null) {
                Debug.Log("Check for player prefab");
            }

            _damage = playerAttack._damage; //Get damage from PlayerAttack class 

            Destroy(gameObject, timeTilDestroy);
        }

        #region Unity Methods
        private void OnTriggerEnter2D(Collider2D collision) {
            //Destroy Upon Hitting Viewport
            if(collision.gameObject.layer == LayerMask.NameToLayer("Viewport")) {
                Destroy(gameObject);
            }

            //Destroy Upon Hitting Enemy 
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                collision.GetComponent<EnemyManager>().TakeDamage(_damage);

                Destroy(gameObject);
            }

            //Destroy Upon Hitting Asteroid
            if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid")) {
                collision.GetComponent<AsteroidManager>().TakeDamage(_damage);

                Destroy(gameObject);
            }

            //Destroy Upon Hitting BossTurret
            if (collision.gameObject.layer == LayerMask.NameToLayer("Turret")) {
                collision.GetComponent<TurretManager>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
        #endregion
    }
}
