using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handler of the SettingsDTO object, which crosses scenes so the settings be
//applied in both the main menu and the pause menu
public class SettingsDTOController : MonoBehaviour
{

    //The value of the master volume
    public float masterValue;

    //The value of the music volume
    public float musicValue;

    //The value of the sfx volume
    public float sfxValue;

    //The value of the brightness (not being used)
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
