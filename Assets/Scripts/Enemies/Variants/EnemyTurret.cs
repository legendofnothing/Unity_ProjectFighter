using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies.Variants {
    public class EnemyTurret : EnemyBehaviour {
        [TitleGroup("Spin Config")] 
        public float spinTime;
        [TitleGroup("Shoot Config")]
        public int volleyAmount;
        public float delayBetweenVolley;
        public float shootingDelay;
        [TitleGroup("Move Config")] 
        public float speed;

        protected override void OnEnter() {
            Spin(spinTime);
        }

        protected override void OnUpdate() {
            Shoot(volleyAmount, delayBetweenVolley, shootingDelay);
            Fly(speed, Vector3.down);
        }

        protected override void OnExit() {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}