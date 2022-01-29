using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
    public bool growUp;
    public float speedUp;
    public float minSize;
    public float maxSize;
    
    public bool growFat;
    public float speedFat;
    public float minWidth;
    public float maxWidth;


    public float minSpeed = 0.001f;
    public float maxSpeed = 0.003f;


	// Use this for initialization
	void Start () {
		minSize = minSize * transform.localScale.z;
		maxSize = maxSize * transform.localScale.z;
		minWidth = minWidth * transform.localScale.x;
		maxWidth = maxWidth * transform.localScale.x;
		minSpeed = minSpeed * transform.localScale.x;
		maxSpeed = maxSpeed * transform.localScale.x;

        Debug.Log($"min: {minSpeed}, max: {maxSpeed}, scale: {transform.localScale.x}");
	}
	
	// Update is called once per frame
	void Update () {
        if (growUp)
        {
            transform.localScale += new Vector3(0, 0, speedUp);
            if(transform.localScale.z >= maxSize)
            {
                growUp = false;
                speedUp = Random.Range(minSpeed, maxSpeed);
            }
        }
        else
        {
            transform.localScale -= new Vector3(0, 0, speedUp);
            if (transform.localScale.z <= minSize)
            {
                growUp = true;
                speedUp = Random.Range(minSpeed, maxSpeed);
            }
        }
        if (growFat)
        {
            transform.localScale += new Vector3(speedFat, 0, 0);
            if (transform.localScale.x >= maxWidth)
            {
                growFat = false;
                speedFat = Random.Range(minSpeed, maxSpeed);
            }
        }
        else
        {
            transform.localScale -= new Vector3(speedFat, 0, 0);
            if (transform.localScale.x <= minWidth)
            {
                growFat = true;
                speedFat = Random.Range(minSpeed, maxSpeed);
            }
        }
    }
}
