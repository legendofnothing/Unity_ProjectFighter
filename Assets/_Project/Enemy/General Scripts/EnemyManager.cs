using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Config")]
    public float enemyHP;
    public float damageDealt;

    private float _damageTimer;
    private float _currHP;
    private bool _isTouching;
    private bool _canDamage = true;

    #region Unity Methods
    void Start() {
        _currHP = enemyHP;
    }
 
    void Update() {
        if (_damageTimer < Time.time && _isTouching) {
            _damageTimer = Time.time + 1f;

            PlayerManager.playerManager.TakeDamage(damageDealt);
        }

        if(_currHP <= 0) {
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
        if (_canDamage) {
            _currHP -= amount;

            StartCoroutine(Iframes());
        }
    }

    IEnumerator Iframes() {
        _canDamage = false;

        yield return new WaitForSeconds(0.4f);

        _canDamage = true;
    }
}