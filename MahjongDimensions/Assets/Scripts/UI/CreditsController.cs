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
            "This project was created by ---",
            "Type of Asset \n \"Asset Name\" \n by Asset Creator",
            "Type of Music \n \"Music Name\" \n by Music Creator on freesound.org",
            "Sound effect \n \"Sound Effect Name\" \n by Sound Effect Creator on freesound.org"
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
