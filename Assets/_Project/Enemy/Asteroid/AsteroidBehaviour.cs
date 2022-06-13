using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidBehaviour : MonoBehaviour
{
    [Header("Enemy Config")]
    public float enemyHP;
    public float damageDealt;

    private float _damageTimer;
    private float _currHP;
    private bool _isTouching;

    #region Unity Methods
    void Start() {
        _currHP = enemyHP;
    }

    void Update() {
        if (_damageTimer < Time.time && _isTouching) {
            _damageTimer = Time.time + 1f;

            PlayerManager.playerManager.TakeDamage(damageDealt);
        }

        if (_currHP <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destroy")) {
            Destroy(gameObject);
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

    public void TakeDamage(float amount) {
        _currHP -= amount;
    }
}