using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTemperature : MonoBehaviour
{
    public float temperature;


    public enum TemperatureType {Normal, Hot, VeryHot, DeadlyHot, Cold, VeryCold, DeadlyCold };


    //Define ranges of TemperatureTypes
    public float maxNormalTemp;

    public float maxHotTemp;

    public float deadlyHotTemp;



    //change temperature by value "change"
    public void ChangeTemperature(float change)
    {
        temperature += change;


        //Adjust Temperature in UI

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



        //If deadly temperature is reached -> game over
        if (IsDead())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

}
