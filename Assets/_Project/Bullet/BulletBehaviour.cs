using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    /* 
     * SUMMARY
     * 
     * Destroy bullet upon hitting certain objects, dealing damage where necessary
     * 
     */

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
            collision.GetComponent<AsteroidBehaviour>().TakeDamage(_damage);

            Destroy(gameObject);
        }
    }
    #endregion
}
