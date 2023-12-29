using System;
using System.Collections;
using System.Collections.Generic;
using Core.Events;
using UnityEngine;
using EventType = Core.Events.EventType;

namespace Weapons {
    public class WeaponManager : MonoBehaviour {
        public List<Weapon> weapons = new List<Weapon>();
        private Weapon _currentWeapon;
        private bool _canReleaseOverheat;
        private Player.Player _player;
        private bool _canAttack; 

        private void Start() {
            _player = GetComponent<Player.Player>();
            _canReleaseOverheat = true;
            _canAttack = true;
            if (weapons.Count <= 0) _currentWeapon = null;
            else {
                foreach (var weapon in weapons) {
                    if (weapons.IndexOf(weapon) == 0) {
                        _currentWeapon = weapon;
                        _currentWeapon.gameObject.SetActive(true);
                    }
                    else weapon.gameObject.SetActive(false);
                }
            }
        }

        private void Update() {
            if (Input.GetKey(KeyCode.Space) && _canAttack) _currentWeapon.OnFire();
            
            if (Input.GetKeyDown(KeyCode.K) && _canReleaseOverheat) StartCoroutine(ReleaseOverheat());

            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);
        }

        private void SwitchWeapon(int index) {
            if (weapons[index] == null) return;
            _currentWeapon.gameObject.SetActive(false);
            _currentWeapon.OnWeaponSwitch();
            _currentWeapon = weapons[index];
            _currentWeapon.gameObject.SetActive(true);
        }

        private IEnumerator ReleaseOverheat() {
            if (_player.currentOverheat <= 0) yield break;
            _canReleaseOverheat = false;
            this.FireEvent(EventType.OnReleaseOverheat);
            yield return new WaitForSeconds(Mathf.Lerp(0, _player.stats.overheatReleaseDuration,
                _player.currentOverheat / _player.stats.overheatCap));
            _player.currentOverheat = 0;
            this.FireEvent(EventType.OnFinishOverheat);
            _canReleaseOverheat = true;
        }

        public void SetCanAttack(int cond) {
            _canAttack = cond == 1;
        }
    }
}
