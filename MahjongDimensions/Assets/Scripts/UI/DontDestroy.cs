using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A simple script used to not destroy a game object across scenes
public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
