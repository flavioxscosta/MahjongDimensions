using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public TileController tileController;

    // Start is called before the first frame update
    void Start()
    {
        tileController.InitializeTileMatrix();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            tileController.ResetTiles();
        }
    }





    

    

    
}
