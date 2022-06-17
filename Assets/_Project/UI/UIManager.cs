using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Scriptable Objects Declaration
    [Header("Scriptable Objects")]
    [SerializeField] private FloatVar playerHP;
    [SerializeField] private FloatVar playerFuel;
    [SerializeField] private FloatVar overHeat;
    [Space]
    [SerializeField] private IntVar pickupRepair;
    [SerializeField] private IntVar pickupFuel;
    [SerializeField] private IntVar pickupHeat;
    [Space]
    [SerializeField] private IntVar score;
    [SerializeField] private IntVar highscore;

    #endregion

    #region Unity Methods
    void Start() {
        StartPlayUI();
        StartUI();
    }
 
    void Update() {
        DisplayStats();

        if (PlayerManager.playerManager._hasDied) {
            StartCoroutine(InitLoseUI());

            if(score.Value > highscore.Value) {
                highscore.Value = score.Value;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_isPausing) {
                Pause(1, false);
            }

            else Pause(0, true);
        }
    }
    #endregion

    #region UIs
    [Space]
    public GameObject LoseUI;
    public GameObject PauseUI;
    public Text scoreDisplayLose;
    public Text highScoreDisplay;

    private bool _isPausing;

    private void StartUI() {
        LoseUI.SetActive(false);
        PauseUI.SetActive(false);

        Time.timeScale = 1;
    }

    private void Pause(int scale, bool state) {
        Time.timeScale = scale;

        PauseUI.SetActive(state);

        _isPausing = state;
    }

    private IEnumerator InitLoseUI() {
        scoreDisplayLose.text = "Score: " + score.Value.ToString();
        highScoreDisplay.text = "Highscore: " + highscore.Value.ToString();

        yield return new WaitForSeconds(1.2f);

        LoseUI.SetActive(true);

        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }
    #endregion

    #region PlayUI
    [Space]
    public Text hpDisplay;
    public Text fuelDisplay;
    public Text heatDisplay;
    [Space]
    public Text pickupDisplay1;
    public Text pickupDisplay2;
    public Text pickupDisplay3;
    [Space]
    public Text scoreDisplay;
    public GameObject[] weaponDisplay;

    private float displayHPprec;
    private float displayFuelprec;
    private GameObject player;
    private PlayerAttack playerAttack;

    private void StartPlayUI() {
        displayHPprec = playerHP.Value;
        displayFuelprec = playerFuel.Value;

        player = GameObject.Find("Player");

        if (player != null) {
            playerAttack = player.GetComponent<PlayerAttack>();
        }

        else
            return;
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

        scoreDisplay.text = score.Value.ToString();

        if(player != null) {
            DisplayWeapons(playerAttack._weaponIndex);
        }
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

    private void DisplayWeapons(int index) {
        for (int i = 0; i < weaponDisplay.Length; i++) {
            if (i == index) {
                weaponDisplay[i].SetActive(true);
            }

            else
                weaponDisplay[i].SetActive(false);
        }
    }

    #endregion

}