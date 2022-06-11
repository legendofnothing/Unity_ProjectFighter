using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private FloatVar _overheat;

    [Header("Shooting Configs")]
    public float overheatCap;
    public float overheatIncreaseAmount;

    [Header("Default Attack")]
    public Transform[] defaultPoints;
    public GameObject defaultBullet;
    public float defaultSpeed;
    public float defaultFR;

    [Header("Shotgun Attack")]
    public Transform[] shotgunPoints;
    public GameObject shotgunBullet;
    public float shotgunSpeed;
    public float shotgunFR;

    //Private Variables
    private float _lastFireTime;
    private float _fireRate; //1 Bullet per fireRate, say if fireRate set to 10 then its 1 bullet/10s
    private float _bulletSpeed;

    private GameObject _bullet;
    private Transform[] _shootPoints;

    private int _weaponIndex = 0;

    //Overheat Related
    private float _overheatTimer;
    private bool _canProduceHeat = true;

    enum WEAPONTYPES {

        DEFAULT = 0,
        SHOTGUN = 1
    }

    #region Unity Methods
    private void Start() {
        ChangeBulletType(defaultBullet, defaultSpeed, defaultFR, defaultPoints);

        _overheat.Value = 0;
    }

    private void Update() {
        Shoot();
        ChangeWeapon();
        InitiateOverheat();

        if(_overheat.Value >= overheatCap) {
            _overheat.Value = overheatCap;
        }
    }
    #endregion

    #region Inputs
    private void Shoot() {
        if(Input.GetKey(KeyCode.J) && Time.time > _lastFireTime) {
            _lastFireTime = Time.time + _fireRate;

            for (int i = 0; i < _shootPoints.Length; i++) {
                var bulletInstance = Instantiate(_bullet, _shootPoints[i].position, _shootPoints[i].rotation);
                bulletInstance.GetComponent<Rigidbody2D>().velocity = _shootPoints[i].transform.up * _bulletSpeed;
            }

            IncreaseOverheat(overheatIncreaseAmount);
        }
    }

    private void ChangeWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeBulletType(defaultBullet, defaultSpeed, defaultFR, defaultPoints);
            _weaponIndex = (int)WEAPONTYPES.DEFAULT;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeBulletType(shotgunBullet, shotgunSpeed, shotgunFR, shotgunPoints);
            _weaponIndex = (int)WEAPONTYPES.SHOTGUN; 
        }
    }

    private void InitiateOverheat() {
        if (Input.GetKeyDown(KeyCode.K) && _overheat.Value >= overheatCap) {
            StartCoroutine(Overheat());
        }
    }

    #endregion

    private void ChangeBulletType(GameObject bullet, float bulletSpeed, float fireRate, Transform[] shootPoints) {
        _bullet = bullet;
        _bulletSpeed = bulletSpeed;
        _fireRate = fireRate;
        _shootPoints = shootPoints;
    } 

    private void IncreaseOverheat(float amount) {
        if (_overheatTimer < Time.time && _canProduceHeat) {
            _overheatTimer = Time.time + 1f;

            _overheat.Value += amount;
        }
    }

    private IEnumerator Overheat() {
        var prevFR      = _fireRate; //Store old fireRate

        _overheat.Value = 0;
        _fireRate      -= 0.1f;
        _canProduceHeat = false;

        yield return new WaitForSeconds(1.4f);

        _fireRate       = prevFR;
        _canProduceHeat = true;
    }
}
