using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private Animator anim;

    public static PlayerManager playerManager { get; private set; }

    [Header("Scriptable Objects")]
    [SerializeField] private FloatVar _playerHP;
    [SerializeField] private FloatVar _playerFuel;

    [Header("Player Configs")]
    public float playerHP;
    public float playerFuel;
    public AnimationClip Hit;
    public AnimationClip Death;

    private float _timer = 0f;
    [HideInInspector] public bool _canDamage = true;

    #region Unity Methods
    private void Awake() {

        if (playerManager != null && playerManager != this) {
            Destroy(this);
        }
        else {
            playerManager = this;
        }
    }

    void Start() {
        _playerHP.Value = playerHP;
        _playerFuel.Value = playerFuel;

        playerAttack = GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();
    }
 
    void Update() {

        //Making sure these values doesnt go to infinity and beyond
        if(_playerHP.Value <= 0) {
            _playerHP.Value = -1;
            StartCoroutine(Die());
            _canDamage = false;

            var isDone = true;

            if (isDone) {
                anim.SetBool("Die", true);
                isDone = false;
            }
        }

        if (_playerFuel.Value <= 0) {
            _playerFuel.Value = -1;
        }

        if (_playerHP.Value > playerHP) {
            _playerHP.Value = playerHP;
        }

        if (_playerFuel.Value > playerFuel) {
            _playerFuel.Value = playerFuel;
        }

        playerAttack._canShoot = _canDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Destroy")) {
            Destroy(gameObject);
        }   
    }
    #endregion

    //Reduce Fuel by amount/1s
    public void ReduceFuel(float amount) {
        if(_timer < Time.time) {
            _timer = Time.time + 1f;

            _playerFuel.Value -= amount;
        }
    }

    public void TakeDamage(float amount) {
        if (_canDamage) {
            _playerHP.Value -= amount;
            StartCoroutine(IFrames());
        }
    }

    //Invincible Frames
    IEnumerator IFrames() {
        _canDamage = false;
        anim.SetTrigger("Hit");

        yield return new WaitForSeconds(Hit.length);

        _canDamage = true;
    }

    //Die
    IEnumerator Die() {
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.GetComponent<PlayerAttack>().enabled = false;

        yield return new WaitForSeconds(Death.length);

        Destroy(gameObject);
    }
}