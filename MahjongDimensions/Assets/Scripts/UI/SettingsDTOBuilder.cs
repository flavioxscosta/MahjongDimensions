using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates a Singleton SettingsDTO object that is sent across scenes
public class SettingsDTOBuilder : MonoBehaviour
{

    //Prefab that the Settings DTO is based on
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject settingsDTO = GameObject.FindGameObjectWithTag("SettingsDTO");
        if (settingsDTO == null)
        {
            GameObject instantiatedObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            instantiatedObject.name = "SettingsDTO";
            instantiatedObject.tag = "SettingsDTO";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
