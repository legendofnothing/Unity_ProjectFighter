using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private FloatVar _playerHP;
    [SerializeField] private FloatVar _playerFuel;

    [Header("Player Configs")]
    public float playerHP;
    public float playerFuel;

    private float _timer = 0f;

    #region Unity Methods
    void Start() {
        _playerHP.Value = playerHP;
        _playerFuel.Value = playerFuel;
    }
 
    void Update() {

        //Making sure these values doesnt go to infinity and beyond
        if(_playerHP.Value <= 0) {
            _playerHP.Value = -1;
        }

        if (_playerFuel.Value <= 0) {
            _playerFuel.Value = -1;
        }

        if (_playerHP.Value > playerHP) {
            _playerHP.Value = playerHP;
        }

        if (_playerFuel.Value > playerFuel) {
            _playerFuel.Value = playerFuel;
        }
    }
    #endregion

    //Reduce Fuel by amount/1s
    public void ReduceFuel(float amount) {
        if(_timer < Time.time) {
            _timer = Time.time + 1f;

            _playerFuel.Value -= amount;
        }
    }
}