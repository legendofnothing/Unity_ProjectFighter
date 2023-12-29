using UnityEngine;

namespace Pickups {
    public class Pickup : MonoBehaviour
    {
        [Header("Scriptable Objects")]
        [SerializeField] private int _pickupRepairKits; //This gives HP
        [SerializeField] private int _pickupFuel; //This gives Fuel
        [SerializeField] private int _pickupHeat; //This gives Overheat

        [Header("Pickup Index [0, 1, 2 - Repair Kits, Fuel, Heat]")]
        public int pickupIndex = 0; 
        #region Unity Methods
        void Start() {
        
        }
 
        void Update() {
        
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
                switch (pickupIndex) {
                    case 0:
                        _pickupRepairKits += 1;
                        Destroy(gameObject);
                        break;
                    case 1:
                        _pickupFuel += 1;
                        Destroy(gameObject);
                        break;
                    case 2:
                        _pickupHeat += 1;
                        Destroy(gameObject);
                        break;
                }
            }

            Destroy(gameObject);
        }
        #endregion
    }
}