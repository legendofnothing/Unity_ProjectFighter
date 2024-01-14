using Bullet;
using UnityEngine;

namespace Weapons {
    public class EnemyWeapon : MonoBehaviour {
        public Transform[] firePoints;
        public WeaponConfig config;
        
        public void Fire() {
            if (firePoints.Length <= 0) return;
            FireLogic();
        }
        
        private void FireLogic() {
            foreach (var firePoint in firePoints) {
                var bulletInstance = Instantiate(config.bullet, firePoint.position, firePoint.rotation);
                bulletInstance.GetComponent<BulletBehaviour>().Init(ref config);
            }
        }
    }
}