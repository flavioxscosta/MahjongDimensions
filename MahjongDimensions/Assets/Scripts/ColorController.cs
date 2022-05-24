using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{

    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public Color color6;
    private Color tempColor;
    private Color[] colorList;
    public Color[] fullColorList;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeColors(int matrixLength)
    {
        color1 = new Color(0.0f, 1.0f, 1.0f);
        color2 = new Color(1.0f, 1.0f, 0.0f);
        color3 = new Color(0.0f, 1, 0f, 0.0f);
        color4 = new Color(1.0f, 0.0f, 0.0f);
        color5 = new Color(1.0f, 0.0f, 1.0f);
        color6 = new Color(1.0f, 0.5f, 0.0f);

        colorList = new Color[] { color1, color2, color3, color4, color5, color6 };

        CreateFullColorList(matrixLength);
        Shuffle();
    }

    void CreateFullColorList(int matrixLength)
    {
        if (matrixLength != 0)
        {
            int totalTiles = (int)Math.Pow(matrixLength, 3.0);
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
            //Must be added in pairs to match up in Mahjong
            fullColorList[i] = colorList[currentColor];
            fullColorList[i + 1] = colorList[currentColor];

            currentColor++;
            if (currentColor >= colorList.Length)
            {
                currentColor = 0;
            }

        }

    }

    public void Shuffle()
    {
        for (int i = 0; i < fullColorList.Length - 1; i++)
        {
            int rnd = UnityEngine.Random.Range(i, fullColorList.Length);
            tempColor = fullColorList[rnd];
            fullColorList[rnd] = fullColorList[i];
            fullColorList[i] = tempColor;
        }
    }
}
