namespace Enemies {
    public class DemoEnemy : EnemyBehaviour {
        protected override void OnEnter() {
            
        }

        protected override void OnUpdate() {
            Shoot(4);
        }

        protected override void OnExit() {
            
        }
    }
}