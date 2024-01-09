using System;
using System.Collections;
using UnityEngine;
using Weapons;

namespace Enemies {
    public abstract class EnemyBehaviour : MonoBehaviour {
        public EnemyWeapon weapon;
        private Rigidbody2D _rigidbody;
        private bool _canShoot = true;
        protected Vector3 PlayerPos;
        protected bool HasDied;

        private void Start() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _canShoot = true;
            if (Player.Player.Instance != null) PlayerPos = Player.Player.Instance.transform.position;
            OnEnter();
        }

        private void Update() {
            if (Player.Player.Instance != null) PlayerPos = Player.Player.Instance.transform.position;
            OnUpdate();
        }

        public void OnDeath() {
            OnExit();
            HasDied = true;
        }

        protected abstract void OnEnter(); 
        protected abstract void OnUpdate(); 
        protected abstract void OnExit();

        protected void LookAt(Vector3 point, float maxRotationDelta) {
            var dir = (point - transform.position).normalized;
            dir.z = 0;
            var rot = Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f, Vector3.forward);
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rot, maxRotationDelta));
        }

        protected void AimAt(Vector3 point) {
            var dir = (point - transform.position).normalized;
            dir.z = 0;
            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f, Vector3.forward);
        }

        protected void FlyForward(float speed) {
            _rigidbody.velocity = transform.up * speed;
        }

        protected void Shoot(int amountOfShots) {
            if (weapon != null && _canShoot) {
                _canShoot = false;
                weapon.Fire(amountOfShots);
                StartCoroutine(FireDelay(weapon.config.fireDelay));
            }
        }

        private IEnumerator FireDelay(float delay) {
            yield return new WaitForSeconds(delay);
            _canShoot = true;
        }
    }
}