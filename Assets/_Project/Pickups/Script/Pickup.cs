using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pickup : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private IntVar _pickupRepairKits; //This gives HP
    [SerializeField] private IntVar _pickupFuel; //This gives Fuel
    [SerializeField] private IntVar _pickupHeat; //This gives Overheat

    [Header("Pickup Index [0, 1, 2 - Repair Kits, Fuel, Heat]")]
    public int pickupIndex = 0; 
    #region Unity Methods
    void Start() {
        
    }
 
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (pickupIndex) {
            case 0:
                _pickupRepairKits.Value += 1;
                Destroy(gameObject);
                break;
            case 1:
                _pickupFuel.Value += 1;
                Destroy(gameObject);
                break;
            case 2:
                _pickupHeat.Value += 1;
                Destroy(gameObject);
                break;
        }
    }
    #endregion
}