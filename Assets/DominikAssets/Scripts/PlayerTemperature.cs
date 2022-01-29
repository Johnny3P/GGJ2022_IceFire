using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


}
