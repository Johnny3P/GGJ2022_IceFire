using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureModifier : MonoBehaviour
{
    //The amount of temperature changed per frame
    public float temperatureChange;

    

    //If player stays in collider, temperature changes
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerTemperature>().canNeutralize = false;

            other.gameObject.GetComponent<PlayerTemperature>().ChangeTemperature(temperatureChange);
        }
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerTemperature>().canNeutralize = false; 
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerTemperature>().canNeutralize = true; ;
        }
    }
}
