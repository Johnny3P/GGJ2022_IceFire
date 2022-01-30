using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    void Update()
    {
        if(Input.GetAxis("Cancel") == 1)
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
        SceneManager.LoadScene("Stefan_Level02");
        Debug.Log("Load Level 2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Stefan_Level01");
        Debug.Log("Load Level 3");
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level_J");
        Debug.Log("Load Level 4");
    }

    public void LoadLevel5()
    {
        SceneManager.LoadScene("Level_FollowProjectile");
        Debug.Log("Load Level 5");
    }

    public void LoadLevel6()
    {
        SceneManager.LoadScene("Level2");
        Debug.Log("Load Level 6");
    }
}
