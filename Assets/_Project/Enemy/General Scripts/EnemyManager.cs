using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    /*  SUMMARY
    * 
    *  Enemy Manager. Manage Enemy Things
    * 
    */

    [Header("Enemy Config")]
    public float enemyHP;
    public float damageDealt;
    public float scoreToAdd;

    private float _damageTimer;
    private float _currHP;
    private bool _isTouching;

    [Space]
    //Audios
    public AudioClip enemyHitAudio;
    public AudioClip enemyDieAudio;

    private EnemyBehaviour enemyBehaviour;

    private Animator anim;
    #region Unity Methods
    void Start() {
        _currHP = enemyHP;

        anim = GetComponent<Animator>();

        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }
 
    void Update() {
        //Deal damageDealt per second
        if (_damageTimer < Time.time && _isTouching) {
            _damageTimer = Time.time + 1f;

            PlayerManager.playerManager.TakeDamage(damageDealt);
        }

        if(_currHP <= 0) {

            StartCoroutine(Die());
        }
    }

    //Destroy Upon Hitting bound
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
    }

    public IEnumerator Die() {
        anim.SetTrigger("Destroy");
        enemyBehaviour.enabled = false;

        if (!gameObject.GetComponent<BoxCollider2D>()) {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }

        else
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return null;
    }


}