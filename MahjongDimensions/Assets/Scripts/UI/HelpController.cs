using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HelpController : MonoBehaviour
{

    string[] helpList;
    public TextMeshProUGUI currentHelpText;
    public GameObject previousButton;
    public GameObject nextButton;

    int i;


    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        FillHelpList();
        currentHelpText.text = helpList[i];
    }

    void FillHelpList()
    {
        helpList = new string[]{
            "Click on identical tiles to match them and remove them from the game!",
            "Only tiles that have two adjancent sides showing can be clicked. Remove all game tiles to win!",
            "You can press R to restart the round and Esc to open the Pause Menu!"
        };
    }

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
