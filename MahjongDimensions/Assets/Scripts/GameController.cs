using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Camera sceneCamera;
    public GameObject tilePrefab;
    int i = 0;
    int matrixLength;
    private GameObject[][][] tileMatrix;

    // Start is called before the first frame update
    void Start()
    {

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

                    //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GameObject instantiatedObject = Instantiate(tilePrefab, new Vector3(i * tilePrefab.transform.lossyScale.x,
                        j * tilePrefab.transform.lossyScale.y, k * tilePrefab.transform.lossyScale.z), Quaternion.Euler(0f, 0f, 0f));
                    instantiatedObject.name = "Tile " + (x+y+z).ToString();
                    MeshRenderer renderer = instantiatedObject.GetComponent<MeshRenderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
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
                    Transform objectHit = hit.transform;
                    Debug.Log(objectHit.gameObject.name);

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
}
