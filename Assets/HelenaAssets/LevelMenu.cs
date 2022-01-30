using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
        Debug.Log("Load Level 1");
    }

    public void LoadLevel2()
    {
        //SceneManager.LoadScene("Level2");
        Debug.Log("Load Level 2");
    }

    public void LoadLevel3()
    {
        //SceneManager.LoadScene("Level3");
        Debug.Log("Load Level 3");
    }

    public void ComingSoon()
    {
        Debug.Log("Coming soon...");
    }
}
