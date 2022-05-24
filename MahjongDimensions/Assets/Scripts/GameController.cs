using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class controls the behavior of the application, such as starting or restarting the game
public class GameController : MonoBehaviour
{

    public TileController tileController;

    // Start is called before the first frame update
    //Creates the cube of Mahjong tiles
    void Start()
    {
        tileController.InitializeTileMatrix();
    }

    // Update is called once per frame
    //Handles key shortcuts, such as restarting the game
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            tileController.ResetTiles();
        }
    }





    

    

    
}
