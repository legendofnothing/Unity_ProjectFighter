using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Unity Methods
    void Start() {
        StartPlayUI();
    }
 
    void Update() {
        DisplayStats();
    }
    #endregion

    #region PlayUI
    [Header("Scriptable Objects")]
    [SerializeField] private FloatVar playerHP;
    [SerializeField] private FloatVar playerFuel;
    [SerializeField] private FloatVar overHeat;
    [Space]
    [SerializeField] private IntVar pickupRepair;
    [SerializeField] private IntVar pickupFuel;
    [SerializeField] private IntVar pickupHeat;

    [Space]
    public Text hpDisplay;
    public Text fuelDisplay;
    public Text heatDisplay;
    [Space]
    public Text pickupDisplay1;
    public Text pickupDisplay2;
    public Text pickupDisplay3;

    private float displayHPprec;
    private float displayFuelprec;

    private void StartPlayUI() {
        displayHPprec = playerHP.Value;
        displayFuelprec = playerFuel.Value;
    }

    private void DisplayStats() {
        //Display Overheat
        if (overHeat.Value > 49f && overHeat.Value < 79f) {
            heatDisplay.color = Color.yellow;
        }

        else if (overHeat.Value > 79f) {
            heatDisplay.color = Color.red;
        }

        else
            heatDisplay.color = Color.white;

        if (overHeat.Value >= 100) {
            heatDisplay.text = "READY";
        }

        else
            heatDisplay.text = overHeat.Value.ToString("0") + "%";

        DisplayPrecent(hpDisplay, displayHPprec, playerHP, "FATAL");
        DisplayPrecent(fuelDisplay, displayFuelprec, playerFuel, "Out");

        DisplayPickups(pickupDisplay1, pickupRepair);
        DisplayPickups(pickupDisplay2, pickupFuel);
        DisplayPickups(pickupDisplay3, pickupHeat);
    }

    private void DisplayPrecent(Text display, float prec, FloatVar value, string word) {
        var precentage = (value.Value/ prec) * 100;

        if (precentage < 30f) {
            display.color = Color.red;
        }

        else if (precentage < 60f) {
            display.color = Color.yellow;
        }

        else
            display.color = Color.white;

        if (precentage <= 0) {
            display.text = word;
        }
        
        else
            display.text = precentage.ToString("0") + "%";
    }

    private void DisplayPickups(Text display, IntVar pickups) {
        if(pickups.Value <= 0) {
            display.text = "NONE";
            display.color = Color.red;
        }

        else {
            display.text = "x" + pickups.Value;
            display.color = Color.white;
        }
    }

    #endregion

}