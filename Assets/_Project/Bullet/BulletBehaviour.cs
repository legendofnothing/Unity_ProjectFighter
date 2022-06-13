using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float _damage;

    private GameObject player;
    private PlayerAttack playerAttack;

    private void Start() {
        player = GameObject.Find("Player");
        playerAttack = player.GetComponent<PlayerAttack>();

        if(player == null) {
            Debug.Log("Check for player prefab");
        }

        _damage = playerAttack._damage;
    }

    #region Unity Methods
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Viewport")) {
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            collision.GetComponent<EnemyManager>().TakeDamage(_damage);

            Destroy(gameObject);
        }
    }
    #endregion
}
