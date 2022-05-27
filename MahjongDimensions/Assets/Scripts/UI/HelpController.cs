using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//A simple class that alternates between horizontal pages of a UI
//Note: This class is identical to CreditsController class. They were initially different classes
//as they had much different behaviour. They remain separate for convenience, but ideally they should be the same class
public class HelpController : MonoBehaviour
{
    //List of text, one will be shown per page
    string[] helpList;

    //UI element that holds text
    public TextMeshProUGUI currentHelpText;

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
        FillHelpList();
        currentHelpText.text = helpList[i];
    }

    //Text to be shown in each page
    void FillHelpList()
    {
        helpList = new string[]{
            "Click on identical tiles to match them and remove them from the game!",
            "Only tiles that have two adjancent sides showing can be clicked. Remove all game tiles to win!",
            "You can press R to restart the round and Esc to open the Pause Menu!"
        };
    }

    //Goes back to the previous page of text
    public void Previous()
    {
        if (i >= helpList.Length - 1)
        {
            nextButton.SetActive(true);

        }

        if (i == 1)
        {
            previousButton.SetActive(false);
        }
        i--;
        currentHelpText.text = helpList[i];

    }

    //Advances to the next page of text
    public void Next()
    {

        if(i == 0)
        {
            previousButton.SetActive(true);

        }

        if (i == helpList.Length - 2)
        {
            nextButton.SetActive(false);
        }
        i++;
        currentHelpText.text = helpList[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
