using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileScript : MonoBehaviour
{


    public Transform projectile;

    public float projectileSpeed;

    public float spawnDistance;

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


            Vector3 direction = new Vector3(shootDirections[directionCount].x * projectileSpeed, shootDirections[directionCount].y * projectileSpeed, shootDirections[directionCount].z);

           Vector3 normDirection = direction.normalized;

            p.position = new Vector3 ( gameObject.transform.position.x + normDirection.x * spawnDistance, gameObject.transform.position.y + normDirection.y * spawnDistance, gameObject.transform.position.z);
           


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
