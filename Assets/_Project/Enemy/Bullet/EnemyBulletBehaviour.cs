using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBulletBehaviour : MonoBehaviour
{
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
            if (PlayerManager.playerManager._canDamage) {
                Destroy(gameObject);

                PlayerManager.playerManager.TakeDamage(10f);
            }
        }
    }
    #endregion
}