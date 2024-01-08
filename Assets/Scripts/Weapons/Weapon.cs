using System;
using System.Collections;
using Bullet;
using Core.Events;
using UnityEngine;
using EventType = Core.Events.EventType;

namespace Weapons {
    [Serializable]
    public struct WeaponConfig {
        public GameObject bullet;
        public float speed;
        public float fireDelay;
        public float damage;
        public float lifespan;
    }
    
    public class Weapon : MonoBehaviour {
        public string name;
        public Transform[] firePoints;
        public WeaponConfig config;

        private bool _canShoot;
        private Player.Player _player;
        private float _currentFireDelay;

        private void Start() {
            _canShoot = true;
            _currentFireDelay = config.fireDelay;
            _player = transform.parent.parent.GetComponent<Player.Player>();
            this.AddListener(EventType.OnReleaseOverheat, _=>OnOverheat());
            this.AddListener(EventType.OnFinishOverheat, _=>OnOverheatFinish());
        }

        public void Fire() {
            if (!_canShoot || firePoints.Length <= 0) return;
            foreach (var firePoint in firePoints) {
                var bulletInstance = Instantiate(config.bullet, firePoint.position, firePoint.rotation);
                bulletInstance.GetComponent<BulletBehaviour>().Init(ref config);
            }
            _player.AddOverheat(_player.stats.overheatIncreaseAmount);
            StartCoroutine(Delay());
        }

        public void OnWeaponSwitch() {
            StopAllCoroutines();
            _canShoot = true;
        }

        private void OnOverheat() {
            _currentFireDelay -= 0.1f;
        }

        private void OnOverheatFinish() {
            _currentFireDelay = config.fireDelay;
        }

        private IEnumerator Delay() {
            _canShoot = false;
            yield return new WaitForSeconds(_currentFireDelay);
            _canShoot = true;
        }
    }
}