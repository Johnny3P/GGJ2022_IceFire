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

    public float groundCheckPauseTime;

    public float cancelJumpPauseTime;

    public float cancelJumpDelayTime;

    private Animator animator;



    private Rigidbody rb;

    private bool canJump;

    private bool isJumping;

    private bool isGrounded;

    private bool groundCheckPauseComplete;

    private bool canCancelJump;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        canJump = true;
        groundCheckPauseComplete = true;
    }

    private void Update()
    {
        if (Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer).Length > 0 && groundCheckPauseComplete)
        {
            isGrounded = true;
            isJumping = false;

            animator.SetTrigger("EndJump");
        }
        else
        {
            isGrounded = false;
        }
  

     
        //Move left/right

        float direction = Input.GetAxis("Horizontal");

        



        if(direction < 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
                }
        else if (direction > 0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
        }


        rb.velocity = new Vector3(direction * moveSpeed, rb.velocity.y, 0);

        animator.SetFloat("Speed", rb.velocity.magnitude);



        //Jump

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x , jumpSpeed , 0);

            isJumping = true;

            animator.SetTrigger("Jump");


            groundCheckPauseComplete = false;

            StartCoroutine(GroundCheckPause());

            //StartCoroutine(CancelJumpPause());


        }


        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            isJumping = false;


            StartCoroutine(CancelJump());


        }



    }


    private IEnumerator GroundCheckPause()
    {
        yield return new WaitForSeconds(groundCheckPauseTime);

        groundCheckPauseComplete = true;
    }

   /* private IEnumerator CancelJumpPause()
    {
        yield return new WaitForSeconds(cancelJumpPauseTime);

        canCancelJump = true;
    }*/

    private IEnumerator CancelJump()
    {
        canCancelJump = false;

        yield return new WaitForSeconds(cancelJumpDelayTime);

        rb.velocity = new Vector3(rb.velocity.x, 0, 0);

        //rb.AddForce(new Vector3(0, cancelSpeed, 0));



    }


}
