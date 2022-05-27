using Platformer.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    /// <summary>
    /// The MetaGameController is responsible for switching control between the high level
    /// contexts of the application, eg the Main Menu and Gameplay systems.
    /// </summary>
    public class MetaGameController : MonoBehaviour
    {
        /// <summary>
        /// The main UI object which used for the menu.
        /// </summary>
        public MainUIController mainMenu;

        /// <summary>
        /// A list of canvas objects which are used during gameplay (when the main ui is turned off)
        /// </summary>
        public Canvas[] gamePlayCanvasii;

        public Canvas gameOverCanvas;
        public Canvas victoryCanvas;


        bool showMainCanvas = false;

        public GameController gameController;

        public TileController tileController;

        private void Start()
        {

        }

        void OnEnable()
        {
            _ToggleMainMenu(showMainCanvas);
        }

        /// <summary>
        /// Turn the main menu on or off.
        /// </summary>
        /// <param name="show"></param>
        public void ToggleMainMenu(bool show)
        {
            if (this.showMainCanvas != show)
            {
                _ToggleMainMenu(show);
            }
        }

        void _ToggleMainMenu(bool show)
        {
            if (show)
            {
                if (Time.timeScale != 0)
                {
                    Time.timeScale = 0;
                    mainMenu.gameObject.SetActive(true);
                    showMainCanvas = show;
                    foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
                }
                   
            }
            else
            {
                Time.timeScale = 1;
                mainMenu.gameObject.SetActive(false);
                showMainCanvas = show;
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(true);
            }
        }

        public void ToggleGameOverMenu(bool show)
        {
            if (show)
            {
                Time.timeScale = 0;
                gameOverCanvas.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                gameOverCanvas.gameObject.SetActive(false);
            }
        }
        public void ToggleVictoryMenu(bool show)
        {
            if (show)
            {
                Time.timeScale = 0;
                victoryCanvas.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                victoryCanvas.gameObject.SetActive(false);
            }
        }

        public void Resume()
        {
            ToggleMainMenu(false);
            tileController.ToggleMatrix(true);
        }

        void Update()
        {

            if (Input.GetButtonDown("Cancel")) //The escape key by default
            {
                HandlePause();
            }

            //Used for testing
            //if (Input.GetKeyDown(KeyCode.G))
            //{
            //    Debug.Log("G");
            //    ToggleGameOverMenu(true);
            //}

            //Used for testing
            //if (Input.GetKeyDown(KeyCode.V))
            //{
            //    Debug.Log("V");
            //    ToggleVictoryMenu(true);
            //}
        }

        public void HandlePause()
        {
            tileController.ToggleMatrix(showMainCanvas);
            ToggleMainMenu(show: !showMainCanvas);
        }

        public void ReturnToMainMenu(bool gameOver)
        {
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        }

        public void RestartLevel()
        {
            Time.timeScale = 1;
            gameController.Reset();
        }


    }
}
