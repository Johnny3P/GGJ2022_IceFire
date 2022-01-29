using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTest : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpSpeed = 100000f;
    [SerializeField] private float distToGround = 200.5f;

    private Rigidbody rb;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool m_IsOnGround;

    private Animator animator;
    private bool groundCheckPauseComplete;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
/*
        if(IsOnGround) {
            Debug.Log("Test");
        } else {
            Debug.Log("Test2");
        }*/

        var velocity = Vector3.forward * -Input.GetAxis("Horizontal") * speed;
        transform.Translate(velocity * Time.deltaTime);
        animator.SetFloat("Speed", velocity.magnitude);

        if(Input.GetAxis("Jump") > 0f && isGrounded && !isJumping) {
            isGrounded = false;
            isJumping = true;
            //transform.Translate(Vector3.up * Input.GetAxis("Jump") * jumpSpeed * Time.deltaTime);
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0);
            animator.SetTrigger("Jump");
            groundCheckPauseComplete = false;
            StartCoroutine(GroundCheckPause());
        }

    }

    void OnCollisionEnter(Collision theCollision) {
        if (groundCheckPauseComplete) {
            Debug.Log("Test");
            isGrounded = true;
            isJumping = false;
            animator.SetTrigger("EndJump");
        }
    }

    private IEnumerator GroundCheckPause() {
        yield return new WaitForSeconds(1);

        groundCheckPauseComplete = true;
    }

}
