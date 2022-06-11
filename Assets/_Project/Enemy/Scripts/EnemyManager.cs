using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    private PlayerManager playerManager;

    private float _damageTimer;
    private bool _isTouching;

    #region Unity Methods
    void Start() {
        playerManager = player.GetComponent<PlayerManager>();
    }
 
    void Update() {
        if (_damageTimer < Time.time && _isTouching) {
            _damageTimer = Time.time + 1f;

            playerManager.TakeDamage(10f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            playerManager.TakeDamage(10f);
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
}