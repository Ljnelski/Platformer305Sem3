using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //"Public Varibles"
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    //Private Variables
    private Rigidbody2D rbody;
    private bool isGrounded = false;
    private Animator anim;
    private bool isFaceingRight = true;

    // Start is called before the first frame update
    void Start()
    {

        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Physics
    void FixedUpdate()
    {

        float horiz = Input.GetAxis("Horizontal");
        isGrounded = GroundCheck();

        //jump code
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            rbody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        //Move code              
        rbody.velocity = new Vector2(horiz * speed, rbody.velocity.y);

        //check if sprite needs to be fliped
        if (isFaceingRight && rbody.velocity.x < 0.0f || !isFaceingRight && rbody.velocity.x > 0.0f)        
            Flip();
        
        

        // Communicate with anim
        anim.SetFloat("xVelocity", Mathf.Abs(rbody.velocity.x));
        anim.SetFloat("yVelocity", rbody.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
               
    }
    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }
    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFaceingRight = !isFaceingRight;
    }
}
