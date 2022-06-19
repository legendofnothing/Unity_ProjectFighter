using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelection;
    public GameObject manual;

    #region Unity Methods
    void Start() {
        levelSelection.SetActive(false);
        manual.SetActive(false);
    }
 
    void Update() {
        
    }
    #endregion
    
    public void StartButton() {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void Manual() {
        mainMenu.SetActive(false);
        manual.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void LoadScene(int scene) {
        SceneManager.LoadScene(scene);
    }

    public void Return() {
        levelSelection.SetActive(false);
        manual.SetActive(false);
        mainMenu.SetActive(true);
    }
}