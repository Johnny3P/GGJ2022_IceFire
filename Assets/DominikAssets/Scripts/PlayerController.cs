using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public float jumpSpeed;

    public float cancelSpeed;

    public LayerMask groundLayer;

    public Transform groundCheck;

    public float groundCheckRadius;

    public float jumpTimeOut;

    private Rigidbody rb;

    private bool canJump;

    private bool isJumping;

    private bool isGrounded;

    private bool jumpTimerComplete;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        canJump = true;
        jumpTimerComplete = true;
    }

    private void FixedUpdate()
    {
        if (Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer).Length > 0 && jumpTimerComplete)
        {
            isGrounded = true;
            isJumping = false;
        }
        else
        {
            isGrounded = false;
        }
  

     
        //Move left/right

        float direction = Input.GetAxis("Horizontal");


        rb.velocity = new Vector3(direction * moveSpeed, rb.velocity.y, 0);


        //Jump

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x , jumpSpeed , 0);

            isJumping = true;

            jumpTimerComplete = false;

            StartCoroutine(WaitAfterJump());


        }


        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);

            //rb.AddForce(new Vector3(0, cancelSpeed, 0));


            isJumping = false;

        }



    }


    private IEnumerator WaitAfterJump()
    {
        yield return new WaitForSeconds(jumpTimeOut);

        jumpTimerComplete = true;
    }
}
