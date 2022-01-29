using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    private Rigidbody rb;


    public List<Transform> destinations;

    public float moveSpeed;

    private int destCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {


        

        rb.MovePosition(transform.position + GetFromToDirection(gameObject.transform.position, destinations[destCount].position));

        if (Vector3.Distance(transform.position, destinations[destCount].position) < 0.1){

            destCount++;

            if(destCount >= destinations.Count)
            {
                destCount = 0;
            }
        }
    }


  


    private Vector3 GetFromToDirection(Vector3 from, Vector3 to)
    {
        Vector3 result = new Vector3(to.x - from.x, to.y - from.y, to.z - from.z);

        result = result.normalized;

        result.x *= moveSpeed;
        result.y *= moveSpeed;

        return result;

       
    }
}
