using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemperature : MonoBehaviour
{
    public float temperature;


    public enum TemperatureType {Normal, Hot, VeryHot, Cold, VeryCold };


    //change temperature by value "change"
    public void ChangeTemperature(float change)
    {
        temperature += change;
    }


}
