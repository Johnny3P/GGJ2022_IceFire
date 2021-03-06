using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTemperature : MonoBehaviour
{
    public float temperature;

    private FMOD.Studio.EventInstance music;

    //Speed with which temperature moves to 0 back
    public float neutralizeSpeed;

    public bool canNeutralize = true;


    public enum TemperatureType {Normal, Hot, VeryHot, DeadlyHot, Cold, VeryCold, DeadlyCold };


    //Define ranges of TemperatureTypes
    public float maxNormalTemp;

    public float maxHotTemp;

    public float deadlyHotTemp;


    private bool isColorChanging = true;
    private bool colorToggle;
    [SerializeField] private Renderer PlayerRenderer;

    private Animator animator;

    [SerializeField] private GameObject explosionPrefab;

    private static bool musicStarted = false;


    private bool hasDied;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (!musicStarted) {
            music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/MainMusic");
            music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            music.start();
            musicStarted = true;
        }

    }

    private void Update()
    {
        

        //Neutralize: Temperature moves slowly back to zero
        if (canNeutralize && Mathf.Abs(temperature) > 1)
        {
            temperature += -Mathf.Sign(temperature) * neutralizeSpeed;
            UpdateTemperatureUI();

        }


        if (hasDied && Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        float temperatureColor = temperature / deadlyHotTemp;
        if (isColorChanging)
        {
            if (temperatureColor > 0f)
            {
                if (temperatureColor > 0.9f)
                {
                    if (colorToggle)
                    {
                        PlayerRenderer.material.SetColor("_Color", new Color(1f, 1, 1, 1));
                        colorToggle = false;
                    }
                    else
                    {
                        PlayerRenderer.material.SetColor("_Color", new Color(1f, 1 - temperatureColor, 1 - temperatureColor, 1));
                        colorToggle = true;
                    }
                    isColorChanging = false;
                    StartCoroutine(ColorBlinking());
                }
                else
                {
                    PlayerRenderer.material.SetColor("_Color", new Color(1f, 1 - temperatureColor, 1 - temperatureColor, 1));
                }
            }
            else
            {
                if (temperatureColor < -0.9f)
                {
                    if (colorToggle)
                    {
                        PlayerRenderer.material.SetColor("_Color", new Color(1f, 1, 1, 1));
                        colorToggle = false;
                    }
                    else
                    {
                        PlayerRenderer.material.SetColor("_Color", new Color(1 - Mathf.Abs(temperatureColor), 1 - Mathf.Abs(temperatureColor), 1, 1));
                        colorToggle = true;
                    }
                    isColorChanging = false;
                    StartCoroutine(ColorBlinking());
                }
                else
                {
                    PlayerRenderer.material.SetColor("_Color", new Color(1 - Mathf.Abs(temperatureColor), 1 - Mathf.Abs(temperatureColor), 1, 1));
                }
            }
        }
    }

    //change temperature by value "change"
    public void ChangeTemperature(float change)
    {
        if (!hasDied)
        {

            temperature += change;

            //Adjust Temperature in UI
            UpdateTemperatureUI();
        }
       



        //If deadly temperature is reached -> game over
        if (!hasDied && IsDead())
        {/*
            music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            music.release();
            */
            hasDied = true;

            Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 0.5f), Quaternion.identity);

            animator.SetTrigger("Death");

            GetComponent<PlayerController>().enabled = false;

            GameObject.Find("GameOverText").GetComponent<Text>().enabled = true;

            

        }

        
        
    }


    public TemperatureType GetTemperatureType()
    {

        if(Mathf.Abs(temperature) < maxNormalTemp)
        {
            return TemperatureType.Normal;
        }
        else if(temperature > 0)
        {
            //Postiive (hot) temperatures
            if (Mathf.Abs(temperature) < maxHotTemp)
            {
                return TemperatureType.Hot;
            }
            else if (Mathf.Abs(temperature) < deadlyHotTemp)
            {
                return TemperatureType.VeryHot;
            }
            else if (Mathf.Abs(temperature) >= deadlyHotTemp)
            {
                return TemperatureType.DeadlyHot;
            }
        }
        else if (temperature < 0)
        {
            //Negative (cold) temperatures
            if (Mathf.Abs(temperature) < maxHotTemp)
            {
                return TemperatureType.Cold;
            }
            else if (Mathf.Abs(temperature) < deadlyHotTemp)
            {
                return TemperatureType.VeryCold;
            }
            else if (Mathf.Abs(temperature) >= deadlyHotTemp)
            {
                return TemperatureType.DeadlyCold;
            }
        }

        return TemperatureType.Normal;

    }

    //Check if deadly temperature is reached
    private bool IsDead()
    {
        if (GetTemperatureType() == TemperatureType.DeadlyCold || GetTemperatureType() == TemperatureType.DeadlyHot)
        {
            return true;
        }

        else return false;
    }


    private void UpdateTemperatureUI()
    {
        Vector3 hotScale = GameObject.Find("HotScale").transform.localScale;
        Vector3 coldScale = GameObject.Find("ColdScale").transform.localScale;


        if (temperature > 0)
        {


            GameObject.Find("HotScale").transform.localScale = new Vector3(hotScale.x, hotScale.y, (temperature / deadlyHotTemp) * 92 + 8);
            GameObject.Find("ColdScale").transform.localScale = new Vector3(coldScale.x, coldScale.y, 8);
        }

        else if (temperature < 0)
        {


            GameObject.Find("ColdScale").transform.localScale = new Vector3(coldScale.x, coldScale.y, (Mathf.Abs(temperature) / deadlyHotTemp) * 92 + 8);
            GameObject.Find("HotScale").transform.localScale = new Vector3(hotScale.x, hotScale.y, 8);
        }
        else if (temperature == 0)
        {
            GameObject.Find("ColdScale").transform.localScale = new Vector3(coldScale.x, coldScale.y, 8);
            GameObject.Find("HotScale").transform.localScale = new Vector3(hotScale.x, hotScale.y, 8);

        }
    }



    private IEnumerator ColorBlinking()
    {
        yield return new WaitForSeconds(0.2f);

        isColorChanging = true;
    }

}
