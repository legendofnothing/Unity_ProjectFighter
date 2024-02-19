using Enemies.Variants;
using UnityEngine;

namespace Enemies {
    public class BossTurret : Enemy {
        private BossTurretBehaviour _bossTurret;
        
        protected override void Start() {
            base.Start();
            _bossTurret = GetComponent<BossTurretBehaviour>();
        }

        public override void OnEnterDeath() {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            _bossTurret.enabled = false;
        }
        
        public override void OnFinishDeath() {
            
        }
    }
}