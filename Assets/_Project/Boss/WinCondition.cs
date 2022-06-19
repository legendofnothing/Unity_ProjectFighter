using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject[] turrets;

    private TurretManager turretManagers;

    [SerializeField] private FloatVar _bossHP;

    private void Awake() {
        _bossHP.Value = 0;
    }

    private void Start() {
        for(int i = 0; i < turrets.Length; i++) {
            turretManagers = turrets[i].GetComponent<TurretManager>();

            _bossHP.Value += turretManagers.turretHP;
        }

    }
    private void Update() {

    }
}
