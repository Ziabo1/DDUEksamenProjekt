using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 8;
    private float jumpingPower = 8;
    private bool isFacingRight = true;

    private bool oneJump = true;
    private bool powerUpDoubleJump = false;
    private bool doubleJump;
   
    private bool PowerUpDash = false;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 5f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    public float thrust = 10f;
    private float dirX = 0f;
    private float Horizontal;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
       

        if (PowerUpDash == false)
        {
          
        }

        horizontal = Input.GetAxisRaw("Horizontal")* speed;

        if (oneJump == true)
        {

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpingPower);
            }

            if (Input.GetButtonUp("Jump") && rb2D.velocity.y > 0f)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
            }

        }

        if (powerUpDoubleJump == true)
        {

            if (IsGrounded() && !Input.GetButton("Jump"))
            {
                doubleJump = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded() || doubleJump)
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpingPower);

                    doubleJump = !doubleJump;
                }
            }

            if (Input.GetButtonUp("Jump") && rb2D.velocity.y > 0f)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) &&  !canDash)
        {
            Dashit();
            Debug.Log("DASH");
        }

        Flip();
    }

    public void Collider2D(Collision2D other)
    {
        if (other.gameObject.name == ("PowerUpJump"))
        {
            Destroy(other.gameObject);

        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);


        if (PowerUpDash == false)
        {
            if (isDashing)
            {
                return;
            }
        }

    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;  
        }
    }
    private void Dashit()
    {
     
        StartCoroutine(Dash());
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb2D.velocity = new Vector2(transform.localScale.x * thrust, 0f);
        tr.emitting = true;
        Debug.Log("CROU ");
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
     
        //dash = false
        // wait x time
        // can dash = true
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "CameraRemoveFromPlayer")
        {
            Debug.Log("Hit ");

        }
    }


}
