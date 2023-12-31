using System;
using Core.Events;
using P = Player;
using UnityEngine;
using EventType = Core.Events.EventType;

namespace Pickups {
    public enum PickupType {
        Repair,
        Fuel,
        Overheat,
    }
    
    public class PickupManager : MonoBehaviour {
        private P.Player _player;
        private int _repairKits;
        private int _fuelCans;
        private int _overheatCans;
        
        #region Unity Methods
        private void Start() {
            _player = GetComponent<P.Player>();
            this.AddListener(EventType.OnPickupAdded, param => AddPickup((PickupType)param));
        }

        private void Update() {
            PickupInput();
        }
        #endregion

        private void PickupInput() {
            if (Input.GetKeyDown(KeyCode.I)) UsePickup(PickupType.Repair);
            else if (Input.GetKeyDown(KeyCode.O)) UsePickup(PickupType.Fuel);
            else if (Input.GetKeyDown(KeyCode.P)) UsePickup(PickupType.Overheat);
        }

        private void AddPickup(PickupType type) {
            switch (type) {
                case PickupType.Repair:
                    _repairKits++;
                    break;
                case PickupType.Fuel:
                    _fuelCans++;
                    break;
                case PickupType.Overheat:
                    _overheatCans++;
                    break;
            }
        }

        private void UsePickup(PickupType type) {
            var hasEnough = type switch {
                PickupType.Fuel => _fuelCans > 0,
                PickupType.Overheat => _overheatCans > 0,
                PickupType.Repair => _repairKits > 0,
            };
            if (!hasEnough) return;
            var reachedCap = type switch {
                PickupType.Fuel => _player.currentHP < _player.stats.playerHp,
                PickupType.Overheat => _player.currentOverheat < _player.stats.overheatCap,
                PickupType.Repair => _player.currentFuel < _player.stats.startingFuel,
            };
            if (!reachedCap) return;
            switch (type) {
                case PickupType.Repair:
                    _player.AddHP(_player.stats.hpAdded);
                    _repairKits--;
                    break;
                case PickupType.Fuel:
                    _player.AddFuel(_player.stats.fuelAdded);
                    _fuelCans--;
                    break;
                case PickupType.Overheat:
                    _player.AddOverheatOnce(_player.stats.overheatAdded);
                    _overheatCans--;
                    break;
            }
        }
    }
}