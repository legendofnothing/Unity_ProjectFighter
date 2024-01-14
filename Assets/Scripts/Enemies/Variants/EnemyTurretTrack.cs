using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies.Variants {
    public class EnemyTurretTrack : EnemyBehaviour {
        [TitleGroup("Shoot Config")]
        [Range(0f, 10f)]
        public float rotateSpeed;
        public int volleyAmount;
        public float delayBetweenVolley;
        public float shootingDelay;
        [TitleGroup("Move Config")] 
        public float speed;

        protected override void OnEnter() {
        }

        protected override void OnUpdate() {
            Shoot(volleyAmount, delayBetweenVolley, shootingDelay);
            Fly(speed, Vector3.down);
            LookAt(PlayerPos, rotateSpeed);
        }

        protected override void OnExit() {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}