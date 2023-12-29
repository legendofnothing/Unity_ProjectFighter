using Player;
using UnityEngine;

namespace Pickups {
    public class PickupManager : MonoBehaviour
    {
        [Header("Scriptable Objects")]
        [SerializeField] private int _pickupRepairKits; //This gives HP
        [SerializeField] private int _pickupFuel; //This gives Fuel
        [SerializeField] private int _pickupHeat; //This gives Overheat
        [Space]
        [SerializeField] private float _playerHP;
        [SerializeField] private float _playerFuel;
        [SerializeField] private float _playerOverheat;

        [Header("Stats Add")]
        public float hpAdd;
        public float fuelAdd;
        public float overheatAdd;

        private GameObject player;
        
        private Player.Player _player;

        private float _maxPlayerHP;
        private float _maxPlayerFuel;
        private float _maxPlayerOverheat;

        #region Unity Methods
        void Start() {
            player = GameObject.Find("Player");
            
            _player = player.GetComponent<Player.Player>();

            _maxPlayerHP       = _player.stats.playerHp;
            _maxPlayerFuel     = _player.stats.startingFuel;

            _pickupRepairKits = 0;
            _pickupFuel       = 0;
            _pickupHeat       = 0;
        }
 
        void Update() {
            UsePickups();
        }
        #endregion

        private void UsePickups() {
            if (Input.GetKeyDown(KeyCode.I)) {
                PickupManaging(_pickupRepairKits, _playerHP, hpAdd, _maxPlayerHP);
            }
        
            if (Input.GetKeyDown(KeyCode.O)) {
                PickupManaging(_pickupFuel, _playerFuel, fuelAdd, _maxPlayerFuel);
            }

            if (Input.GetKeyDown(KeyCode.P)) {
                PickupManaging(_pickupHeat, _playerOverheat, overheatAdd, _maxPlayerOverheat);
            }
        }

        private void PickupManaging(int pickup, float value, float amountAdd, float cap) {
            if(value < cap && pickup > 0) {
                pickup -= 1;

                value  += amountAdd;
            }
        }
    }
}