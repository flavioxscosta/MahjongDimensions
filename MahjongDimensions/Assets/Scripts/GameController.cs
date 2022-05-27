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
    public Text pointsText;

    //Time left in the round. Player loses the round if they cannot match all the tiles before the timer reaches zero
    public float timeRemaining = 300;

    //UI element that displays how much time is left in the round
    public Text timeText;

    // Start is called before the first frame update
    //Creates the cube of Mahjong tiles
    void Start()
    {
        tileController.InitializeTileMatrix();
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
            points = 0;
            UpdatePoints();
            tileController.ResetTiles();
        }
    }





    

    

    
}
