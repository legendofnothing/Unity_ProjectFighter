using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Weapons;

namespace Enemies {
    public abstract class EnemyBehaviour : MonoBehaviour {
        public EnemyWeapon weapon;
        private Rigidbody2D _rigidbody;
        private bool _canShoot = true;
        private Tween _spinTween;
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
            _spinTween?.Kill();
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

        protected void Fly(float speed, Vector3 dir) {
            _rigidbody.velocity = dir * speed;
        }

        protected void Spin(float spinTime) {
            _spinTween = transform.DOLocalRotate(new Vector3(0, 0, 360), spinTime, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

        protected void Shoot(int volleyAmount, float delayBetweenVolley, float shootingDelay) {
            if (weapon != null && _canShoot) {
                StartCoroutine(FireLogic(volleyAmount, delayBetweenVolley, shootingDelay));
            }
        }

        private IEnumerator FireLogic(int volleyAmount, float delayBetweenVolley, float shootingDelay) {
            _canShoot = false;
            for (var i = 0; i < volleyAmount; i++) {
                weapon.Fire();
                yield return new WaitForSeconds(delayBetweenVolley); 
            }
            yield return new WaitForSeconds(shootingDelay);
            _canShoot = true;
        }
    }
}