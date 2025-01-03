using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void goMainGame()
    {
        SceneManager.LoadScene("Main");
    }
}
