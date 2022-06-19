using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour
{
    /*  SUMMARY
     * 
     *  Asteroid Behaviours, Including dealing damage, destroy upon HP = 0 or reach the bound "Destroy"
     * 
     */

    [Header("Enemy Config")]
    public float enemyHP;
    public float damageDealt;
    public float scoreToAdd;

    [Space]
    //Audios
    public AudioClip enemyHitAudio;
    public AudioClip enemyDieAudio;

    private Animator anim;

    private float _damageTimer;
    private float _currHP;
    private bool _isTouching;

    #region Unity Methods
    void Start() {
        _currHP = enemyHP;
        anim = GetComponent<Animator>();
    }

    void Update() {
        //Damage player by damageDealt per seconds
        if (_damageTimer < Time.time && _isTouching) {
            _damageTimer = Time.time + 1f;

            PlayerManager.playerManager.TakeDamage(damageDealt);
        }

        //On HP = 0
        if (_currHP <= 0) {
            anim.SetTrigger("Destroy");
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
        PlayerManager.playerManager.AddScore(scoreToAdd);
        AudioManager.instance.PlaySoundEffect(enemyHitAudio, 0.3f);
    }

    public void Die() {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        AudioManager.instance.PlaySoundEffect(enemyDieAudio, 0.3f);
    }
}