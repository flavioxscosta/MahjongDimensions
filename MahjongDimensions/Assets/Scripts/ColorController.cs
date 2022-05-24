using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles the creation and randomization of colors to be used in each tile
public class ColorController : MonoBehaviour
{

    //All the different colors tiles can have, placeholders to the different symbols in the final version
    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public Color color6;

    //List of all the above colors
    private Color[] colorList;

    //Randomized list of all colors. Its size equals the number of tiles so that
    // each color can be applied to each tile
    public Color[] fullColorList;

    //Initializes the colors to be used and shuffles the color that each tile will have
    public void Initialize(int matrixLength)
    {
        InitializeColors();
        InitializeFullColorList(matrixLength);
        Shuffle();
    }

    //Initializes the list of possible colors
    void InitializeColors()
    {
        color1 = new Color(0.0f, 1.0f, 1.0f);
        color2 = new Color(1.0f, 1.0f, 0.0f);
        color3 = new Color(0.0f, 1, 0f, 0.0f);
        color4 = new Color(1.0f, 0.0f, 0.0f);
        color5 = new Color(1.0f, 0.0f, 1.0f);
        color6 = new Color(1.0f, 0.5f, 0.0f);

        colorList = new Color[] { color1, color2, color3, color4, color5, color6 };
    }

    //Initializes the list with the color of each tile
    void InitializeFullColorList(int matrixLength)
    {
        if (matrixLength != 0)
        {
            //Since tile formation is a cube, figuring out the number of tiles is trivial, but
            //for more complex shapes a different method would be required
            int totalTiles = (int)Math.Pow(matrixLength, 3.0);

            //Number of total tiles must be even so there are now stray tiles at the end
            if (totalTiles % 2 != 0)
            {
                Debug.LogError("Total number of tiles is not even!");

            }
            else
            {
                fullColorList = new Color[totalTiles];
            }
        }

        int currentColor = 0;

        for (int i = 0; i < fullColorList.Length - 1; i = i + 2)
        {
            //Must be added in pairs so there are no stray tiles at the end
            fullColorList[i] = colorList[currentColor];
            fullColorList[i + 1] = colorList[currentColor];

            currentColor++;
            if (currentColor >= colorList.Length)
            {
                currentColor = 0;
            }

        }

    }

    //Randomizes the list of colors for each tile so that each game is different
    public void Shuffle()
    {
        for (int i = 0; i < fullColorList.Length - 1; i++)
        {
            int rnd = UnityEngine.Random.Range(i, fullColorList.Length);
            Color tempColor = fullColorList[rnd];
            fullColorList[rnd] = fullColorList[i];
            fullColorList[i] = tempColor;
        }
    }
}
