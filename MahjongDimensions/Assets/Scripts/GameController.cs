using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Camera sceneCamera;
    public GameObject tilePrefab;
    int i = 0;
    int matrixLength = 4;
    private GameObject[][][] tileMatrix;
    private GameObject selectedTile;

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
                    selectedTile = objectHit.gameObject;
                    Debug.Log(selectedTile.name);
                    Outline outline = selectedTile.GetComponent<Outline>();
                    if(outline != null)
                    {
                        outline.enabled = true;
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
        color6 = new Color(1.0f, 1.0f, 0.0f);

        colorList = new Color[]{color1, color2, color3, color4, color5, color6};

        CreateFullColorList();
        Shuffle();
        Debug.Log(fullColorList);
    }

    void CreateFullColorList()
    {
        Debug.Log(matrixLength);
        if (matrixLength != 0)
        {

            int totalTiles = (int)Math.Pow(matrixLength, 3.0);
            if(totalTiles % 2 != 0)
            {
                Debug.Log("Total number of tiles is not even!");

            } else
            {
                fullColorList = new Color[totalTiles];
            }

            
        }

        int currentColor = 0;
        Debug.Log(colorList);

        for (int i = 0; i < fullColorList.Length; i++)
        {
            //Must be added in pairs to match up in Mahjong
            fullColorList[i] = colorList[currentColor];
            fullColorList[i] = colorList[currentColor];

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
}
