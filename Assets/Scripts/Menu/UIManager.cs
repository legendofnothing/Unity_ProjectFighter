using System.Collections;
using Player;
using ScriptableObjects;
using SpawnPattern;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu {
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
        [SerializeField] private FloatVar bossHP;

        #endregion

        #region Unity Methods
        [Space]
        private GameObject gameManager;
        private SpawnerManager spawnerManager;
        void Start() {
            StartPlayUI();
            StartUI();

            gameManager = GameObject.Find("GameManager");

            if(gameManager != null) {
                spawnerManager = gameManager.GetComponent<SpawnerManager>();
            }
        }
 
        void Update() {
            DisplayStats();

            if (PlayerManager.playerManager._hasDied) {
                StartCoroutine(InitLoseUI());
                AudioManager.instance.audioSource.Stop();

                if (score.Value > highscore.Value) {
                    highscore.Value = score.Value;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (_isPausing) {
                    Pause(1, false);
                }

                else Pause(0, true);
            }

            if(gameManager != null) {
                if(bossHP.Value <= 0 && spawnerManager.hasStarted) {

                    StartCoroutine(InitWinUI());
                    AudioManager.instance.audioSource.Stop();
                }
            }
        }
        #endregion

        #region UIs
        [Space]
        public GameObject LoseUI;
        public GameObject PauseUI;
        public GameObject WinUI;

        public Text scoreDisplayLose;
        public Text highScoreDisplay;

        public Text scoreDisplayWin;
        public Text highScoreDisplayWin;
        public Text rank;

        public AudioClip loseAudio;
        public AudioClip winAudio;

        private bool _isPausing;

        private void StartUI() {
            LoseUI.SetActive(false);
            PauseUI.SetActive(false);
            WinUI.SetActive(false);

            Time.timeScale = 1;
        }

        private void Pause(int scale, bool state) {
            Time.timeScale = scale;

            PauseUI.SetActive(state);

            _isPausing = state;

            if(state == true) {
                AudioManager.instance.audioSource.Pause();
            }

            else
                AudioManager.instance.audioSource.Play();
        }

        private IEnumerator InitLoseUI() {
            scoreDisplayLose.text = "Score: " + score.Value.ToString();
            highScoreDisplay.text = "Highscore: " + highscore.Value.ToString();

            yield return new WaitForSeconds(1.2f);

            LoseUI.SetActive(true);
            AudioManager.instance.PlaySoundTrack(loseAudio, 0.5f);

            yield return new WaitForSeconds(2f);
            Time.timeScale = 0;
        }

        private IEnumerator InitWinUI() {
            WinUI.SetActive(true);
            scoreDisplayWin.text = "Score: " + score.Value.ToString(); 
            highScoreDisplayWin.text = "Highscore: " + highscore.Value.ToString();

            SetRank();
            AudioManager.instance.PlaySoundTrack(winAudio, 0.5f);

            yield return new WaitForSeconds(0.2f);

            Time.timeScale = 0;
        }

        private void SetRank() {
            if ( score.Value > 49999) {
                rank.text = "S"; rank.color = Color.yellow;
            }

            else if (score.Value < 53999 && score.Value > 49999) {
                rank.text = "A"; rank.color = Color.green;
            }

            else if (score.Value < 49999 && score.Value > 39999) {
                rank.text = "B"; rank.color = Color.blue;
            }

            else if (score.Value < 39999 && score.Value > 29999) {
                rank.text = "C"; rank.color = Color.white;
            }

            else if (score.Value < 29999 && score.Value > 19999) {
                rank.text = "D"; rank.color = Color.grey;
            }

            else if (score.Value < 19999) {
                rank.text = "F"; rank.color = Color.red;
            }
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

        #region Buttons
        public void Pausing() {
            Pause(0, true);
        }

        public void Continue() {
            Pause(1, false);
        }

        public void Retry() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit() {
            SceneManager.LoadScene("Menu");
        }
        #endregion

    }
}