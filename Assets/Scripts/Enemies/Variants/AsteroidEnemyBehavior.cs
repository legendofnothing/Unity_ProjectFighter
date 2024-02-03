using Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies.Variants {
    public class AsteroidEnemyBehavior : EnemyBehaviour {
        [TitleGroup("Config")] 
        public float speed;
        public float damage;
        public LayerMask damageLayer;
        [Space]
        public float minAngleDeviation;
        public float maxAngleDeviation;
        
        protected override void OnEnter() {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(180 - minAngleDeviation, 180 + maxAngleDeviation));
        }

        protected override void OnUpdate() {
            Fly(speed, transform.up);
        }

        protected override void OnExit() {
            speed = 0;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (CheckLayerMask.IsInLayerMask(other.gameObject, damageLayer)) {
                GetComponent<Enemy>().TakeDamage(99999);
                Player.Player.Instance.TakeDamage(damage);
            }
        }
    }
}