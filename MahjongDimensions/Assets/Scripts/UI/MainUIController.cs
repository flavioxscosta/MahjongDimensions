using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    //A simple controller for switching between UI panels.
    public class MainUIController : MonoBehaviour
    {
        //All panels of the menu
        public GameObject[] panels;

        //Changes the panel that is showing in the menu
        public void SetActivePanel(int index)
        {
            for (var i = 0; i < panels.Length; i++)
            {
                var active = i == index;
                var g = panels[i];
                if (g.activeSelf != active) g.SetActive(active);
            }
        }

        //Triggers when menu is enabled, showing the main manu
        void OnEnable()
        {
            SetActivePanel(0);
            
        }

        //Starts the game, changing scenes
        public void Play()
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
        }

        //Quits the application (not being used as it is a web application)
        public void Quit()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}