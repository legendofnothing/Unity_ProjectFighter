using Core;
using Enemies;
using UnityEngine;
using Weapons;

namespace Bullet {
    public class BulletBehaviour : MonoBehaviour {
        private WeaponConfig config;
        public LayerMask interactLayer;
        public LayerMask damageLayer; 

        public void Init(ref WeaponConfig config) {
            this.config = config;
            GetComponent<Rigidbody2D>().AddForce(transform.up * config.speed, ForceMode2D.Impulse);
            Destroy(gameObject, config.lifespan);
        }
        
        private void OnTriggerEnter2D(Collider2D collision) {
            if(CheckLayerMask.IsInLayerMask(collision.gameObject, interactLayer)) {
                Destroy(gameObject);
                if(CheckLayerMask.IsInLayerMask(collision.gameObject, damageLayer)) {
                    if (collision.gameObject.TryGetComponent<Enemy>(out var enemy)) enemy.TakeDamage(config.damage);
                    else if (collision.gameObject.TryGetComponent<Player.Player>(out var player)) player.TakeDamage(config.damage);
                } 
            } 
        }
    }
}

