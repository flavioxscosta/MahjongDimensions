using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles the creation and randomization of colors to be used in each tile
public class MaterialController : MonoBehaviour
{

    //All the different materials tiles can have
    public Material[] materialList;

    //Randomized list of all materials. Its size equals the number of tiles so that
    // each material can be applied to each tile
    public Material[] fullMaterialList;

    //Initializes the materials to be used and shuffles the materials that each tile will have
    //matrixLength - total number of tiles
    public void Initialize(int matrixLength)
    {
        InitializeFullMaterialList(matrixLength);
        Shuffle();
    }

    //Initializes the list with the material of each tile
    //matrixLength - total number of tiles
    void InitializeFullMaterialList(int matrixLength)
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
                fullMaterialList = new Material[totalTiles];
            }
        }

        int currentMaterial = 0;

        for (int i = 0; i < fullMaterialList.Length - 1; i = i + 2)
        {
            //Must be added in pairs so there are no stray tiles at the end
            fullMaterialList[i] = materialList[currentMaterial];
            fullMaterialList[i + 1] = materialList[currentMaterial];

            currentMaterial++;
            if (currentMaterial >= materialList.Length)
            {
                currentMaterial = 0;
            }

        }

    }

    //Randomizes the list of materials for each tile so that each game is different
    public void Shuffle()
    {
        for (int i = 0; i < fullMaterialList.Length - 1; i++)
        {
            int rnd = UnityEngine.Random.Range(i, fullMaterialList.Length);
            Material tempColor = fullMaterialList[rnd];
            fullMaterialList[rnd] = fullMaterialList[i];
            fullMaterialList[i] = tempColor;
        }
    }
}
