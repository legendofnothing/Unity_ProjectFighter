using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Configs")]
    public Transform[] defaultPoints;
    public Transform[] shotgunPoints;

    [Header("Default Attack")]
    public GameObject defaultBullet;
    public float defaultSpeed;
    public float defaultFR;

    [Header("Shotgun Attack")]
    public GameObject shotgunBullet;
    public float shotgunSpeed;
    public float shotgunFR;

    private float _lastFireTime;
    private float _fireRate; //1 Bullet per fireRate, say if fireRate set to 10 then its 1 bullet/10s
    private float _bulletSpeed;

    private GameObject _bullet;
    private Transform[] _shootPoints;

    private int _weaponIndex = 0;

    enum WEAPONTYPES {

        DEFAULT = 0,
        SHOTGUN = 1
    }

    #region Unity Methods
    private void Start() {
        ChangeBulletType(defaultBullet, defaultSpeed, defaultFR, defaultPoints);
    }

    private void Update() {
        Shoot();
        ChangeWeapon();
    }
    #endregion

    private void Shoot() {
        if(Input.GetKey(KeyCode.J) && Time.time > _lastFireTime) {
            _lastFireTime = Time.time + _fireRate;

            for (int i = 0; i < _shootPoints.Length; i++) {
                var bulletInstance = Instantiate(_bullet, _shootPoints[i].position, _shootPoints[i].rotation);
                bulletInstance.GetComponent<Rigidbody2D>().velocity = _shootPoints[i].transform.up * _bulletSpeed;
            }
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

    private void ChangeBulletType(GameObject bullet, float bulletSpeed, float fireRate, Transform[] shootPoints) {
        _bullet = bullet;
        _bulletSpeed = bulletSpeed;
        _fireRate = fireRate;
        _shootPoints = shootPoints;
    } 
}
