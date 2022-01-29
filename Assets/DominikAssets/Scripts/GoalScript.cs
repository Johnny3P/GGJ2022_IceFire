using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalScript : MonoBehaviour
{
    public string nextSceneName;

    private bool waitForButtonPress;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waitForButtonPress = true;
            GameObject.Find("LevelCompleteText").GetComponent<Text>().enabled = true;
        }
    }

    private void Update()
    {

        if (waitForButtonPress && Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
