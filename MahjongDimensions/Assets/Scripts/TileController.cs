using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Camera sceneCamera;
    public ColorController colorController;
    public GameObject tilePrefab;
    int matrixLength = 4;
    private GameObject[][][] tileMatrix;
    private GameObject selectedTile;
    private GameObject previousTile;
    private Vector3 selectedCoords;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftClick();
        }
    }

    void HandleLeftClick()
    {
        GameObject clickedTile = GetClickedTile();
        if(clickedTile != null)
        {
            DeselectTile();
            if (CanBeSelected(clickedTile))
            {
                SelectTile(clickedTile);

                if (TilesMatch())
                {
                    CompleteMatch();
                }
            }
        }
    }

    GameObject GetClickedTile()
    {
        RaycastHit hit;
        if (sceneCamera != null)
        {
            Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                Transform objectHit = hit.transform;
                if(objectHit != null)
                {
                    return objectHit.gameObject;
                }
            }
        }
        return null;
    }

    void DeselectTile()
    {
        //Remove outline of previously selected tile
        if (selectedTile != null)
        {
            Outline previousOutline = selectedTile.GetComponent<Outline>();
            if (previousOutline != null)
            {
                previousOutline.enabled = false;
                previousTile = selectedTile;
                selectedTile = null;

            }
        }
    }

    bool CanBeSelected(GameObject tile)
    {
        if (tile.tag.Equals("Tile"))
        {
            
            Vector3? tileCoords = FindCoordsInMatrix(tile);
            if (tileCoords.HasValue)
            {
                Vector3 coords = tileCoords.Value;
                return !IsBlocked(tile, coords);
            }
        }

        return false;
    }

    //Finds the position of a tile in the tile matrix
    //Since "Vector3?" is a nullable type, the result will be null if the 
    //tile is not in the matrix
    Vector3? FindCoordsInMatrix(GameObject tile)
    {
        for (int i = 0; i < tileMatrix.Length; i++)
        {
            for (int j = 0; j < tileMatrix[i].Length; j++)
            {
                for (int k = 0; k < tileMatrix[i][j].Length; k++)
                {
                    if (tile.Equals(tileMatrix[i][j][k]))
                    {
                        return new Vector3(i, j, k);
                    }
                }
            }
        }
        return null;
    }

    bool IsBlocked(GameObject tile, Vector3 tileCoords)
    {
        float tileSize = tile.transform.lossyScale.x;
        float x = tileCoords.x;
        float y = tileCoords.y;
        float z = tileCoords.z;

        //Check all directions around tile. If at least two touching faces (horizontally) are showing, tile can be selected
        //Faces here are labeled as front, back, left and right. In code these are arbitrary as they would differ depending
        //on the player's camera orientation. They are given these names here for better code readability.
        int facesShown = 0;
        bool frontShowing = false;
        bool backShowing = false;
        bool leftShowing = false;
        bool rightShowing = false;

        if (!TileExistsInCoords(new Vector3(x + tileSize, y, z)))
        {
            frontShowing = true;
            facesShown++;
        }
        if (!TileExistsInCoords(new Vector3(x - tileSize, y, z)))
        {
            backShowing = true;
            facesShown++;
        }
        if (!TileExistsInCoords(new Vector3(x, y, z + tileSize)))
        {
            leftShowing = true;
            facesShown++;
        }
        if (!TileExistsInCoords(new Vector3(x, y, z - tileSize)))
        {
            rightShowing = true;
            facesShown++;
        }

        //The only scenario where a tile cannot be selected in this case is if the only 2 faces shown are opposite of each other
        if (facesShown == 2 && frontShowing && backShowing
            || facesShown == 2 && rightShowing && leftShowing)
        {
            return true;
        }

        if (facesShown >= 2)
        {
            return false;
        }

        return true;
    }

    bool TileExistsInCoords(Vector3 coords)
    {

        int x = (int)coords.x;
        int y = (int)coords.y;
        int z = (int)coords.z;

        //Check if coords are within matrix bounds
        //Technically "y" does not need to be checked as it is not being used
        if (x >= 0 && x < tileMatrix.Length &&
            y >= 0 && y < tileMatrix[0].Length &&
            z >= 0 && z < tileMatrix[0][0].Length)
        {
            if (tileMatrix[x][y][z] != null)
            {
                return true;
            }
        }
        return false;
    }

    void SelectTile(GameObject clickedTile)
    {
        selectedTile = clickedTile;
        Debug.Log(selectedTile.name);
        OutlineTile(selectedTile);
    }

    void OutlineTile(GameObject tile)
    {
        Outline outline = tile.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    bool TilesMatch()
    {
        if (previousTile != null && previousTile != selectedTile)
        {
            MeshRenderer previousRenderer = previousTile.GetComponent<MeshRenderer>();
            MeshRenderer selectedRenderer = selectedTile.GetComponent<MeshRenderer>();

            if (previousRenderer != null && selectedRenderer != null)
            {
                if (previousRenderer.material.color == selectedRenderer.material.color)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void CompleteMatch()
    {
        Destroy(previousTile);
        Destroy(selectedTile);
        previousTile = null;
        selectedTile = null;
    }

    public void ResetTiles()
    {
        colorController.Shuffle();
        DestroyMatrix();
        InitializeTileMatrix();
    }

    public void InitializeTileMatrix()
    {
        matrixLength = 4;
        colorController.InitializeColors(matrixLength);
        
        tileMatrix = new GameObject[matrixLength][][];
        for (int i = 0; i < tileMatrix.Length; i++)
        {
            tileMatrix[i] = new GameObject[matrixLength][];
            for (int j = 0; j < tileMatrix[i].Length; j++)
            {
                tileMatrix[i][j] = new GameObject[matrixLength];
                for (int k = 0; k < tileMatrix[i][j].Length; k++)
                {
                    
                    float tileSize = tilePrefab.transform.lossyScale.x;
                    GameObject instantiatedObject = Instantiate(tilePrefab, new Vector3(i * tileSize, j * tileSize, k * tileSize), Quaternion.identity);

                    int currentTileNum = i * tileMatrix.Length * tileMatrix[i].Length + j * tileMatrix[i].Length + k;
                    instantiatedObject.name = "Tile " + currentTileNum.ToString();

                    MeshRenderer renderer = instantiatedObject.GetComponent<MeshRenderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = colorController.fullColorList[currentTileNum];
                    }

                    tileMatrix[i][j][k] = instantiatedObject;
                }
            }
        }
    }


    void DestroyMatrix()
    {
        for (int i = 0; i < tileMatrix.Length; i++)
        {
            for (int j = 0; j < tileMatrix[i].Length; j++)
            {
                for (int k = 0; k < tileMatrix[i][j].Length; k++)
                {
                    if ((tileMatrix[i][j][k] != null))
                    {
                        Destroy(tileMatrix[i][j][k]);
                    }
                }
            }
        }
    }

}
