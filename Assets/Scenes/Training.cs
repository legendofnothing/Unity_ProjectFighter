using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Training : MonoBehaviour
{
    public Text[] movementsText;
    public Text[] shootingText;

    public GameObject[] tutorials;
    public Text aaa;

    #region Unity Methods
    void Start() {
    }
 
    void Update() {
        SetText(KeyCode.A, movementsText[0]);
        SetText(KeyCode.D, movementsText[1]);
        SetText(KeyCode.W, movementsText[2]);
        SetText(KeyCode.S, movementsText[3]);
        SetText(KeyCode.LeftShift, movementsText[4]);

        SetText(KeyCode.Space, shootingText[0]);
        SetText(KeyCode.Alpha1, shootingText[1]);
        SetText(KeyCode.Alpha2, shootingText[2]);
        SetText(KeyCode.Alpha3, shootingText[3]);
        SetText(KeyCode.L, shootingText[4]);
        SetText(KeyCode.K, shootingText[5]);
        SetText(KeyCode.I, shootingText[6]);
        SetText(KeyCode.O, shootingText[7]);
        SetText(KeyCode.P, shootingText[8]);

        if(CheckIfFinished(shootingText) && CheckIfFinished(movementsText)) {
            tutorials[0].SetActive(false);
            tutorials[1].SetActive(false);

            aaa.text = "Training Completed, if unsure check manual in PAUSE MENU, or at MENU SCREEN. Hit ESCAPE to exit or PAUSE BUTTON in Bottom Right Corner";
        }
    }
    #endregion
    private void SetText(UnityEngine.KeyCode keycode, Text text) {
        if (Input.GetKeyDown(keycode)) {
            
            text.color = Color.green;
        }
    }
    private bool CheckIfFinished(Text[] texts) {
        int nums = 0;
        
        for (int i = 0; i < texts.Length; i++) {
            if(texts[i].color == Color.green) {
                nums++;
            }
        }

        if (nums >= texts.Length) {
            return true;
        }

        else return false;
    }
}