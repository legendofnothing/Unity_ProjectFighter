using Sirenix.OdinInspector;

namespace Enemies.Variants {
    public class EnemyChaseShootBehaviour : EnemyChaseBehavior {
        [TitleGroup("Shooting Config")] 
        public int volleyAmount;
        public float delayBetweenVolley;
        public float shootingDelay;
        
        protected override void OnUpdate() {
            base.OnUpdate();
            Shoot(volleyAmount, delayBetweenVolley, shootingDelay);
        }
    }
}