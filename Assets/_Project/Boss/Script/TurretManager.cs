using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretManager : MonoBehaviour
{
    public float turretHP;
    public AnimationClip bossStart;
    public float scoreToAdd;

    [Space]
    //Audios
    public AudioClip enemyHitAudio;
    public AudioClip enemyDieAudio;

    private float currHP;
    private Animator anim;

    private float _damageTimer;
    private bool _isTouching;
    private bool _hasBossStart;

    private TurretBehaviour turretBehaviour;

    [SerializeField] private FloatVar bossHP;

    #region Unity Methods

    void Start() {
        currHP = turretHP;
        anim = GetComponent<Animator>();
        turretBehaviour = GetComponent<TurretBehaviour>();

        StartCoroutine(WaitForBossStart());
    }
 
    void Update() {
        if(currHP <= 0) {
            var isDone = true;

            if (isDone) {
                anim.SetBool("Die", true);
            }
        }

        if (_damageTimer < Time.time && _isTouching && _hasBossStart) {
            _damageTimer = Time.time + 1f;

            PlayerManager.playerManager.TakeDamage(10f);
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

    public void TakeDamage(float dmg) {
        if (_hasBossStart) {
            currHP -= dmg;
            bossHP.Value -= dmg;
            anim.SetTrigger("Hit");

            PlayerManager.playerManager.AddScore(scoreToAdd);
            AudioManager.instance.PlaySoundEffect(enemyHitAudio, 0.3f);
        }
    }

    public void Die() {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        AudioManager.instance.PlaySoundEffect(enemyDieAudio, 0.3f);
    }

    IEnumerator WaitForBossStart() {
        turretBehaviour.enabled = false;
        yield return new WaitForSeconds(bossStart.length);
        turretBehaviour.enabled = true;
        _hasBossStart = true;
    }
}