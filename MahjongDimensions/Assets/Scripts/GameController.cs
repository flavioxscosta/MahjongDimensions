using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Camera sceneCamera;
    public GameObject tilePrefab;
    int matrixLength = 4;
    private GameObject[][][] tileMatrix;
    private GameObject selectedTile;
    private Vector3 selectedCoords;

    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public Color color6;
    private Color tempColor;
    private Color[] colorList;
    private Color[] fullColorList;
     

    // Start is called before the first frame update
    void Start()
    {
        InitializeColors();

        matrixLength = 4;
        tileMatrix = new GameObject[matrixLength][][];
        for (int i = 0; i < tileMatrix.Length; i++)
        {
            tileMatrix[i] = new GameObject[matrixLength][];
            for (int j = 0; j < tileMatrix[i].Length; j++)
            {
                tileMatrix[i][j] = new GameObject[matrixLength];
                for (int k = 0; k < tileMatrix[i][j].Length; k++)
                {

                    int x = i * tileMatrix.Length * tileMatrix[i].Length;
                    int y = j * tileMatrix[i].Length;
                    int z = k;
                    int currentTileNum = x + y + z;

                    //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GameObject instantiatedObject = Instantiate(tilePrefab, new Vector3(i * tilePrefab.transform.lossyScale.x,
                        j * tilePrefab.transform.lossyScale.y, k * tilePrefab.transform.lossyScale.z), Quaternion.Euler(0f, 0f, 0f));
                    instantiatedObject.name = "Tile " + currentTileNum.ToString();
                    MeshRenderer renderer = instantiatedObject.GetComponent<MeshRenderer>();
                    if (renderer != null)
                    {
                        //renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                        renderer.material.color = fullColorList[currentTileNum];
                    }

                    tileMatrix[i][j][k] = instantiatedObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(sceneCamera != null){
                Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    //Remove outline of previously selected tile
                    if (selectedTile != null)
                    {
                        Outline previousOutline = selectedTile.GetComponent<Outline>();
                        if (previousOutline != null)
                        {
                            previousOutline.enabled = false;
                            
                        }
                    }


                    Transform objectHit = hit.transform;
                    GameObject previousTile = selectedTile;
                   

                    if (CanBeSelected(objectHit.gameObject))
                    {
                        selectedTile = objectHit.gameObject;
                        Debug.Log(selectedTile.name);
                        Outline outline = selectedTile.GetComponent<Outline>();
                        if (outline != null)
                        {
                            outline.enabled = true;
                        }

                        if (previousTile != null && previousTile != selectedTile)
                        {
                            
                            MeshRenderer previousRenderer = previousTile.GetComponent<MeshRenderer>();
                            MeshRenderer selectedRenderer = selectedTile.GetComponent<MeshRenderer>();

                            if (previousRenderer != null && selectedRenderer != null)
                            {
                                if (previousRenderer.material.color == selectedRenderer.material.color)
                                {

                                    Destroy(previousTile);
                                    Destroy(selectedTile);
                                    previousTile = null;
                                    selectedTile = null;

                                }
                                
                                
                            }
                        }



                    }
                    else
                    {
                        selectedTile = null;
                    }
                   



                }
            }
            
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //GameObject tile = tileMatrix[1][0][1];
            //Debug.Log(tile);
            //Debug.Log(tileMatrix[1][0][1]);
            //Destroy(tile);
            //Debug.Log(tile);
            //Debug.Log(tileMatrix[1][0][1]);
        }
    }

    void InitializeColors()
    {
        color1 = new Color(0.0f, 1.0f, 1.0f);
        color2 = new Color(1.0f, 1.0f, 0.0f);
        color3 = new Color(0.0f, 1,0f, 0.0f);
        color4 = new Color(1.0f, 0.0f, 0.0f);
        color5 = new Color(1.0f, 0.0f, 1.0f);
        color6 = new Color(1.0f, 0.5f, 0.0f);

        colorList = new Color[]{color1, color2, color3, color4, color5, color6};

        CreateFullColorList();
        Shuffle();
    }

    void CreateFullColorList()
    {
        if (matrixLength != 0)
        {

            int totalTiles = (int)Math.Pow(matrixLength, 3.0);
            if(totalTiles % 2 != 0)
            {
                Debug.LogError("Total number of tiles is not even!");

            } else
            {
                fullColorList = new Color[totalTiles];
            }

            
        }

        int currentColor = 0;

        for (int i = 0; i < fullColorList.Length-1; i = i+2)
        {
            //Must be added in pairs to match up in Mahjong
            fullColorList[i] = colorList[currentColor];
            fullColorList[i+1] = colorList[currentColor];

            currentColor++;
            if (currentColor >= colorList.Length)
            {
                currentColor = 0;
            }
            
        }
        
    }

    void Shuffle()
    {
        for (int i = 0; i < fullColorList.Length - 1; i++)
        {
            int rnd = UnityEngine.Random.Range(i, fullColorList.Length);
            tempColor = fullColorList[rnd];
            fullColorList[rnd] = fullColorList[i];
            fullColorList[i] = tempColor;
        }
    }

    bool CanBeSelected(GameObject tile)
    {
        if (tile.tag.Equals("Tile"))
        {
            int facesShown = 0;
            Vector3? tileCoords = FindCoordsInMatrix(tile);
            if (tileCoords.HasValue)
            {
                float tileSize = tile.transform.lossyScale.x;
                float x = tileCoords.Value.x;
                float y = tileCoords.Value.y;
                float z = tileCoords.Value.z;

                //Check all directions around tile. If at least two touching faces (horizontally) are showing, tile can be selected
                //Faces here are labeled as front, back, left and right. In code these are arbitrary as they would differ depending
                //on the player's camera orientation. They are given these names here for better code readability.
                bool frontShowing = false;
                bool backShowing = false;
                bool leftShowing = false;
                bool rightShowing = false;

                if (!TileExistsInCoords(new Vector3 (x + tileSize, y, z)))
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
                if(facesShown == 2 && frontShowing && backShowing
                    || facesShown == 2 && rightShowing && leftShowing)
                {
                    return false;
                }
    
                if(facesShown >= 2)
                {
                    return true;
                }

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
                    if(tile.Equals(tileMatrix[i][j][k]))
                    {
                        return new Vector3(i, j, k);
                    }
                }
            }
        }

        return null;
    }

    bool TileExistsInCoords(Vector3 coords)
    {

        int x = (int)coords.x;
        int y = (int)coords.y;
        int z = (int)coords.z;

        //Check if coords are within matrix bounds
        //technically "y" should never be a problem as it is not being used
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
}
