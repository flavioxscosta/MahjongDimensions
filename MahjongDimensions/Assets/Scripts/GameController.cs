using Platformer.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class controls the behavior of the application, such as starting or restarting the game
public class GameController : MonoBehaviour
{
    //Controller that handles tile behaviour
    public TileController tileController;

    //Points the player has acquired in a round by matching tiles
    int points = 0;

    //Increase in points whenever the player matches two tiles
    public readonly int pointIncrease = 100;

    //UI element that displays how many points the player has
    public TMPro.TextMeshProUGUI pointsText;

    //How much time a round takes. Player loses the round if they cannot match all the tiles before the timer reaches zero
    public float totalTime = 300;

    //Time left in the round
    float timeRemaining;

    //UI element that displays how much time is left in the round
    public TMPro.TextMeshProUGUI timeText;

    //Controller that handles UI windows' behavior
    public MetaGameController metaGameController;

    //Audio that plays when player fails the round
    public AudioSource gameOverAudio;

    //Audio that plays when player wins the round
    public AudioSource victoryAudio;

    //State of the game. Mostly used to avoid unnecessary Update behaviors while game is over
    bool gameOver;

    // Start is called before the first frame update
    //Creates the cube of Mahjong tiles
    void Start()
    {
        gameOver = false;
        tileController.InitializeTileMatrix();
        timeRemaining = totalTime;
    }

    //Increases the points the player has, using the pointIncrease constant
    //Called when two tiles match (TileController)
    public void IncreasePoints()
    {
        points += pointIncrease;
        UpdatePoints();
    }

    //Updates the points UI text element 
    public void UpdatePoints()
    {
        pointsText.text = "Points: " + points;
    }

    //Decreases the time left in the round. Called every frame
    void DecreaseTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimer();
        } else
        {
            if (!gameOver)
            {
                GameOver();
            }
           
        }
    }

    //Updates the timer UI text element 
    void UpdateTimer()
    {
        float time = timeRemaining + 1; //prevents timer from ending at -1 seconds
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    //Handles key shortcuts, such as restarting the game
    void Update()
    {
        DecreaseTimer();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    //Resets the whole round
    public void Reset()
    {
        gameOver = false;
        points = 0;
        timeRemaining = totalTime;
        UpdatePoints();
        tileController.ResetTiles();
    }

    //Handles the logic of failing a round. Triggered when player runs out of time
    void GameOver()
    {
        gameOver = true;
        gameOverAudio.Play();
        metaGameController.ToggleGameOverMenu(true);
    }

    //Handles the logic of winning a round. Triggered when player matches all tiles
    public void Victory()
    {
        victoryAudio.Play();
        metaGameController.ToggleVictoryMenu(true);
    }










}
