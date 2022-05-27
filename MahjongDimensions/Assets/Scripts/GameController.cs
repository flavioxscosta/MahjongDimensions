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

    //UI elements that displays how many points the player has
    public Text pointsText;

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

    // Update is called once per frame
    //Handles key shortcuts, such as restarting the game
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            points = 0;
            UpdatePoints();
            tileController.ResetTiles();
        }
    }





    

    

    
}
