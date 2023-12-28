using Player;
using ScriptableObjects;
using UnityEngine;

namespace Pickups {
    public class PickupManager : MonoBehaviour
    {
        [Header("Scriptable Objects")]
        [SerializeField] private IntVar _pickupRepairKits; //This gives HP
        [SerializeField] private IntVar _pickupFuel; //This gives Fuel
        [SerializeField] private IntVar _pickupHeat; //This gives Overheat
        [Space]
        [SerializeField] private FloatVar _playerHP;
        [SerializeField] private FloatVar _playerFuel;
        [SerializeField] private FloatVar _playerOverheat;

        [Header("Stats Add")]
        public float hpAdd;
        public float fuelAdd;
        public float overheatAdd;

        private GameObject player;

        private PlayerAttack playerAttack;
        private PlayerManager playerManager;

        private float _maxPlayerHP;
        private float _maxPlayerFuel;
        private float _maxPlayerOverheat;

        #region Unity Methods
        void Start() {
            player = GameObject.Find("Player");

            playerAttack  = player.GetComponent<PlayerAttack>();
            playerManager = player.GetComponent<PlayerManager>();

            _maxPlayerHP       = playerManager.playerHP;
            _maxPlayerFuel     = playerManager.playerFuel;
            _maxPlayerOverheat = playerAttack.overheatCap;

            _pickupRepairKits.Value = 0;
            _pickupFuel.Value       = 0;
            _pickupHeat.Value       = 0;
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

        private void PickupManaging(IntVar pickup, FloatVar value, float amountAdd, float cap) {
            if(value.Value < cap && pickup.Value > 0) {
                pickup.Value -= 1;

                value.Value  += amountAdd;
            }
        }
    }
}