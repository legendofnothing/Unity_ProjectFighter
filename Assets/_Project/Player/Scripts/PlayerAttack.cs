using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Configs")]
    public Transform shootingPoint;
    public float fireRate; //1 Bullet per fireRate, say if fireRate set to 10 then its 1 bullet/10s

    [Header("Default Attack")]
    public GameObject defaultBullet;
    public float bulletSpeed;

    private float _lastFireTime;

    #region Unity Methods
    private void Start() {
        
    }

    private void Update() {
        Shoot();
    }
    #endregion

    void Shoot() {
        if(Input.GetKey(KeyCode.J) && Time.time > _lastFireTime) {
            _lastFireTime = Time.time + fireRate;

            var bulletInstance = Instantiate(defaultBullet, shootingPoint.position, shootingPoint.rotation);
            bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        }
    }
}
