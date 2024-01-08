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

        public void Fire(int amountOfShots) {
            if (firePoints.Length <= 0) return;
            for (var i = 0; i < amountOfShots; i++) {
                FireLogic();
            }
        }

        private void FireLogic() {
            foreach (var firePoint in firePoints) {
                var bulletInstance = Instantiate(config.bullet, firePoint.position, firePoint.rotation);
                bulletInstance.GetComponent<BulletBehaviour>().Init(ref config);
            }
        }
    }
}