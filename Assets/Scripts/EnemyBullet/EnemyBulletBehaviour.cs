using Player;
using UnityEngine;

namespace EnemyBullet {
    public class EnemyBulletBehaviour : MonoBehaviour
    {
        /*  SUMMARY
    * 
    *  Destroy bullet upon hitting certain objects, dealing damage where necessary
    * 
    */
        #region Unity Methods
        void Start() {
        
        }
 
        void Update() {
        
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Destroy")) {
                Destroy(gameObject);
            }

            else if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
                if (Player.Player.Instance.canDamage) {
                    Destroy(gameObject);

                    Player.Player.Instance.TakeDamage(10f);
                }
            }
        }
        #endregion
    }
}