using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public GameObject showTutorialButton;
    public GameObject closeTutorialButton;
    public GameObject tutorialInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showTutorial()
    {
        tutorialInfo.SetActive(true);
    }

    public void closeTutorial()
    {
        tutorialInfo.SetActive(false);
    }
}
