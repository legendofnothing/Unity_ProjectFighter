using System;
using System.Collections;
using Core.Events;
using Core.Patterns;
using ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using EventType = Core.Events.EventType;

namespace Player {
    [Serializable]
    public struct PlayerStat {
        public float playerHp;
        [TitleGroup("Speed Config")] 
        public float normalSpeed;
        public float accelSpeed;
        [TitleGroup("Fuel Config")] 
        public float startingFuel;
        public float fuelBurnOnAccel;
        [TitleGroup("Overheat Config")] 
        public float overheatCap;
        public float overheatIncreaseAmount;
        public float overheatReleaseDuration;
        [TitleGroup("Pickup Config")] 
        public float hpAdded;
        public float fuelAdded;
        public float overheatAdded;
    }
    
    public class Player : Singleton<Player> {
        [Header("Player Configs")] 
        public PlayerData data;

        [TitleGroup("Debug")]
        [ReadOnly] public float currentHP;
        [ReadOnly] public float currentSpeed;
        [ReadOnly] public float currentFuel;
        [ReadOnly] public float currentOverheat;
        [Space]
        [ReadOnly] public bool canDamage = true;
        [ReadOnly] public bool hasDied;
        [ReadOnly] public bool canAddOverheat = true; 
        public PlayerStat stats { private set; get; } 

        private float _fuelTimer;
        private float _overheatTimer;   
        private Animator _animator;
        
        private static readonly int Hit = Animator.StringToHash("Hit");

        #region Unity Methods
        private void Start() {
            stats = data.stats;
            canDamage = true;
            hasDied = false;
            canAddOverheat = true;
            
            currentHP = stats.playerHp;
            currentSpeed = stats.normalSpeed;
            currentFuel = stats.startingFuel;
            
            _animator = GetComponent<Animator>();
        }
        
        #endregion
        
        public void TakeDamage(float amount) {
            if (!canDamage) return;
            currentHP -= amount;
            if(currentHP <= 0) Death();
            else _animator.SetTrigger(Hit);
        }

        public void ChangeCanDamage(int cond) {
            canDamage = cond == 1;
        }
        
        private void Death() {
            currentHP = -1;
            canDamage = false;
            hasDied = true;
            this.FireEvent(EventType.OnPlayerDeath);
        }
        
        public void ReduceFuel(float amount) {
            if(_fuelTimer < Time.time) {
                _fuelTimer = Time.time + 1f;
                if (currentFuel <= 0) currentFuel = 0;
                else currentFuel -= amount;
            }
        }

        public void AddFuel(float amount) {
            if (currentFuel >= stats.startingFuel) return;
            currentFuel += amount;
            if (currentFuel >= stats.startingFuel) currentFuel = stats.startingFuel;
        }
        
        public void AddOverheat(float amount) {
            if(_overheatTimer < Time.time && canAddOverheat) {
                _overheatTimer = Time.time + 1f;
                if (currentOverheat >= stats.overheatCap) currentOverheat = stats.overheatCap;
                else currentOverheat += stats.overheatIncreaseAmount;
            }
        }
        
        public void AddOverheatOnce(float amount) {
            if (currentOverheat >= stats.overheatCap) return;
            currentOverheat += amount;
            if (currentOverheat >= stats.overheatCap) currentFuel = stats.overheatCap;
        }

        public void AddHP(float amount) {
            if (currentHP >= stats.playerHp) return;
            currentHP += amount;
            if (currentHP >= stats.playerHp) currentHP = stats.playerHp;
        }
        
        public void AddScore(float amount) {
            
        }
    }
}