using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDTOController : MonoBehaviour
{

    public float masterValue;
    public float musicValue;
    public float sfxValue;
    public float brightnessValue;

    // Start is called before the first frame update
    void Start()
    {
        masterValue = 100;
        musicValue = 100;
        sfxValue = 100;
        brightnessValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
