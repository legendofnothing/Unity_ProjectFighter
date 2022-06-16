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
    public Text hpDisplay;
    public Text fuelDisplay;
    public Text heatDisplay;

    private float displayHPprec;
    private float displayFuelprec;
    private float displayHeatprec;

    private void StartPlayUI() {
        displayHPprec = playerHP.Value;
        displayFuelprec = playerFuel.Value;
    }

    private void DisplayStats() {
        var prec1 = (playerHP.Value / displayHPprec) * 100;
        var prec2 = (playerFuel.Value / displayFuelprec) * 100;
        var prec3 = overHeat.Value;

        hpDisplay.text = prec1.ToString("0") + "%";
        fuelDisplay.text = prec2.ToString("0") + "%";
        heatDisplay.text = prec3.ToString("0") + "%";

        //HP Display
        if (prec1 < 30f) {
            hpDisplay.color = Color.red;
        }

        else if (prec1 < 60f) {
            hpDisplay.color = Color.yellow;
        }

        else hpDisplay.color = Color.white;

        if (prec1 <= 0) {
            hpDisplay.text = "FATAL";
        }

        else hpDisplay.text = prec1.ToString("0") + "%";

        //Fuel Display
        if (prec2 < 21f) {
            fuelDisplay.color = Color.red;
        }

        else if (prec2 < 51f) {
            fuelDisplay.color = Color.yellow;
        }

        else fuelDisplay.color = Color.white;

        if (prec2 <= 0) {
            fuelDisplay.text = "OUT";
        }

        else fuelDisplay.text = prec1.ToString("0") + "%";
    }
    #endregion

}