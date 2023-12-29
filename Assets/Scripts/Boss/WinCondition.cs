using UnityEngine;

namespace Boss {
    public class WinCondition : MonoBehaviour
    {
        public GameObject[] turrets;

        private TurretManager turretManagers;

        [SerializeField] private float _bossHP;

        private void Awake() {
            _bossHP = 0;
        }

        private void Start() {
            for(int i = 0; i < turrets.Length; i++) {
                turretManagers = turrets[i].GetComponent<TurretManager>();

                _bossHP += turretManagers.turretHP;
            }

        }
        private void Update() {

        }
    }
}
