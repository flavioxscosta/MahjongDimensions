using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//A simple class that alternates between horizontal pages of a UI
//Note: This class is identical to HelpController class. They were initially different classes
//as they had much different behaviour. They remain separate for convenience, but ideally they should be the same class
public class CreditsController : MonoBehaviour
{
    //List of text, one will be shown per page
    string[] creditsList;

    //UI element that holds text
    public TextMeshProUGUI currentCreditsText;

    //Button that goes back in the text
    public GameObject previousButton;

    //Button that advances text
    public GameObject nextButton;

    //Current page position
    int i;


    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        FillCreditsList();
        currentCreditsText.text = creditsList[i];
    }

    //Text to be shown in each page
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

    //Goes back to the previous page of text
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

    //Advances to the next page of text
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
