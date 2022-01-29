using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileScript : MonoBehaviour
{


    public Transform projectile;

    public float projectileSpeed;

    //List of shootDirections, z-axis contains time between shots!
    public List<Vector3> shootDirections;


    //Number of current direction in the list
    private int directionCount;


    private bool canShoot = true;

  
    // Update is called once per frame
    void Update()
    {

        if (canShoot)
        {
            Transform p = Instantiate(projectile) as Transform;

            p.position = gameObject.transform.position;

            Vector3 direction = new Vector3(shootDirections[directionCount].x * projectileSpeed, shootDirections[directionCount].y * projectileSpeed, shootDirections[directionCount].z);

            p.GetComponent<Rigidbody>().velocity = direction;

            directionCount++;

            if(directionCount >= shootDirections.Count)
            {
                directionCount = 0;
            }

            canShoot = false;

            StartCoroutine(WaitUntilNextShot(direction.z));

        }
        
    }


    private IEnumerator WaitUntilNextShot(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        canShoot = true;


    }
}
