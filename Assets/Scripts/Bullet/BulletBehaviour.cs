using Asteroid;
using Boss;
using Enemy;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Bullet {
    public class BulletBehaviour : MonoBehaviour {
        private WeaponConfig config;

        public void Init(ref WeaponConfig config) {
            this.config = config;
            GetComponent<Rigidbody2D>().AddForce(transform.up * config.speed, ForceMode2D.Impulse);
            Destroy(gameObject, config.lifespan);
        }
        
        private void OnTriggerEnter2D(Collider2D collision) {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Viewport")) {
                Destroy(gameObject);
            } 
        }
    }
}
