using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    string[] creditsList;
    public TextMeshProUGUI currentCreditsText;
    public GameObject previousButton;
    public GameObject nextButton;

    int i;


    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        FillCreditsList();
        currentCreditsText.text = creditsList[i];
    }

    void FillCreditsList()
    {
        creditsList = new string[]{
            "This project was created by Flávio Costa",
            "Tile Outlines\n \"Quick Outline\" \n by Chris Nolet on the Unity Asset Store",
            "Flower Sprites\n \"Icon Pack: Flowers | Flat\" \n by Vitaly Gorbachev on Flaticon",
            "Background music\n \"Wrapped in dreams - base track 02\" \n by frankum on freesoung.org",
            "Sound effect \n \"Retro video game sfx - Jump\" \n by OwlStorm on freesound.org",
            "Sound effect \n \"retro death sfx3.wav\" \n by stumpbutt on freesound.org",
            "Sound effect \n \"hup.wav\" \n by maxmakessounds on freesound.org"
        };
    }

    public void Previous()
    {

        if (i >= creditsList.Length - 1)
        {
            nextButton.SetActive(true);

        }

        if (i == 1)
        {
            previousButton.SetActive(false);
        }
        i--;
        currentCreditsText.text = creditsList[i];

    }



    public void Next()
    {

        if (i == 0)
        {
            previousButton.SetActive(true);

        }

        if (i == creditsList.Length - 2)
        {
            nextButton.SetActive(false);
        }
        i++;
        currentCreditsText.text = creditsList[i];



    }



    // Update is called once per frame
    void Update()
    {

    }
}
