using UnityEngine;

namespace Enemies {
    public class Enemy : MonoBehaviour {
        [Header("Enemy Config")]
        public float enemyHP;
        
        private float _currHP;
        private bool _hasDied;
        private BoxCollider2D _collider;
        private Animator _animator;
        private EnemyBehaviour _behaviour;
        
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Hit = Animator.StringToHash("Hit");

        protected virtual void Start() {
            _behaviour = GetComponent<EnemyBehaviour>();
            _currHP = enemyHP;
            _animator = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
        }

        public void TakeDamage(float amount) {
            if (_hasDied) return;
            _currHP -= amount;
            if (_animator) _animator.SetTrigger(Hit);
            if (_currHP <= 0) Die();
        }
        
        private void Die() {
            _hasDied = true;
            if (_animator) _animator.SetTrigger(Death);
            if (_behaviour != null) _behaviour.OnDeath();
        }

        public virtual void OnEnterDeath() {
            _collider.enabled = false;
        }

        public virtual void OnFinishDeath() {
            Destroy(gameObject);
        }
    }
}