using Platformer.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    //The MetaGameController is responsible for switching control between the high level
    //contexts of the application, eg the Main Menu and Gameplay systems.
    public class MetaGameController : MonoBehaviour
    {
        //The main UI object which used for the menu.
        public MainUIController mainMenu;

        //A list of canvas objects which are used during gameplay (when the main ui is turned off)
        public Canvas[] gamePlayCanvasii;

        //Canvas that shows up when player fails a round
        public Canvas gameOverCanvas;

        //Canvas that shows up when player wins a round
        public Canvas victoryCanvas;

        //Whether the pause menu should be showing or not
        bool showMainCanvas = false;

        //Controller that handles general game behaviour
        public GameController gameController;

        //Controller that handles tile behaviour
        public TileController tileController;

        //
        void OnEnable()
        {
            _ToggleMainMenu(showMainCanvas);
        }

        //Turns the pause menu on or off. Checks if menu needs to be shown
        //show - whether the pause menu should be shown or hidden
        public void ToggleMainMenu(bool show)
        {
            if (this.showMainCanvas != show)
            {
                _ToggleMainMenu(show);
            }
        }

        //Turns the pause menu on or off.
        //show - whether the pause menu should be shown or hidden
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

        //Turns the game over menu on or off.
        //show - whether the menu should be shown or hidden
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

        //Turns the victory over menu on or off.
        //show - whether the menu should be shown or hidden
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

        //Resumes the game, hiding the menu and showing the tiles
        public void Resume()
        {
            ToggleMainMenu(false);
            tileController.ToggleMatrix(true);
        }

        //Called every frame
        void Update()
        {
            if (Input.GetButtonDown("Cancel")) //The escape key by default
            {
                HandlePause();
            }
        }

        //Handles the player pressing the pause key or button, toggling the pause menu and tiles
        public void HandlePause()
        {
            tileController.ToggleMatrix(showMainCanvas);
            ToggleMainMenu(show: !showMainCanvas);
        }

        //Returns to the Main Menu scene
        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        }

        //Restarts the round, unpausing the game
        public void RestartLevel()
        {
            Time.timeScale = 1;
            gameController.Reset();
        }


    }
}
