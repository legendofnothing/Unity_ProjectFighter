using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies.Variants {
    public class BossTurret : EnemyBehaviour {
        public Rigidbody2D turretTarget;
        [Range(0f, 10f)]
        public float rotateSpeed;
        [TitleGroup("Shoot Config")] 
        public int volleyAmount;
        public float delayBetweenVolley;
        public float shootingDelay;
        
        protected override void OnEnter() {
        }

        protected override void OnUpdate() {
            if (turretTarget) LookAt(PlayerPos, rotateSpeed, turretTarget);
            Shoot(volleyAmount, delayBetweenVolley, shootingDelay);
        }

        protected override void OnExit() {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}   