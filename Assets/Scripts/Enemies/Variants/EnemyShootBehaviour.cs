using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies.Variants {
    public class EnemyShootBehaviour : EnemyBehaviour {
        [TitleGroup("ShootFly Config")] 
        public float speed;
        [Space]
        public int volleyAmount;
        public float delayBetweenVolley;
        public float shootingDelay;
        
        protected override void OnEnter() {
        }

        protected override void OnUpdate() {
            Fly(speed, transform.up);
            Shoot(volleyAmount, delayBetweenVolley, shootingDelay);
        }

        protected override void OnExit() {
        }
    }
}
