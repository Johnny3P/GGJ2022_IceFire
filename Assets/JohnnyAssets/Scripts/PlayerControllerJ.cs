using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJ : MonoBehaviour
{
    private bool isJumping;

    private bool isGrounded;


    public float runSpeed = 10f;
    public float jumpForce = 1000f;
    public float horizontalInput;
    
   
    public Rigidbody playerRb;
    public float gravityScale = 40f;
    public float fallingGravityScale = 200f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ----- this is where we get the players input
        horizontalInput = Input.GetAxis("Horizontal");
       
        // ----------move the player forward with translate
        transform.Translate(Vector3.right * Time.deltaTime * runSpeed * horizontalInput);
        // transform.Translate(Vector3.up * Time.deltaTime * floatSpeed * verticalInput);

        playerRb.AddForce(Physics.gravity * (gravityScale - 1));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            Debug.Log("Space key was pressed.");
        }

        // falling down faster then upwards
        if (playerRb.velocity.y >= 0)
        {
            playerRb.mass = gravityScale;
        }
        else if (playerRb.velocity.y < 0)
        {
            playerRb.mass = fallingGravityScale;
        }
    }
}
