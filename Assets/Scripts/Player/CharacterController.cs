using System;
using DG.Tweening;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public enum PlayerStates
    {
        Walking,
        Reflecting,
        Dashing,
        Wave
    }
    
    [Header("Basic Movement")] 
    
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 0.25f;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private bool isGrounded;
    public PlayerStates playerState;
    public float gravity;
    public float waveGravity;
    private bool facingRight = true;

    [Header("Dash")] 
    
    public float dashValue;
    public float startDashTime;
    private float dashTime;
    private int direction;
    private bool isDashEnabled;

    [Header("Animation")] [SerializeField] private Animator playerAnimator;
    

    private void Start()
    {
        playerState = PlayerStates.Walking;
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        float h = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("speed",h);
        switch (playerState)
        {
            case PlayerStates.Walking:
                Gravity();
                Movement(h);
                if (Input.GetButton("Jump") && isGrounded) playerAnimator.SetTrigger("jump");
                Flip(h);                
                break;
            case PlayerStates.Reflecting:
                rigidbody.velocity = Vector3.zero;
                rigidbody.useGravity = false;
                break;
            case PlayerStates.Wave:
                isDashEnabled = true;
                WaveGravity();
                Dash();
                break;
        }
    }

    private void Movement(float h)
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            rigidbody.velocity = new Vector3(h * speed * 1.5f, rigidbody.velocity.y, 0f);
        }
        else
        {
            rigidbody.velocity = new Vector3(h * speed, rigidbody.velocity.y, 0f);
        }
    }

    private void Flip(float h)
    {
        if (h > 0)
        {
            transform.DORotate(Vector3.zero, rotationSpeed);
            facingRight = true;
        }
        else if (h < 0)
        {
            transform.DORotate(new Vector3(0, 180, 0), rotationSpeed);
            facingRight = false;
        }
    }

    private void Jump()
    {
        
        rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.3f, ground);
        playerAnimator.SetBool("isGrounded",isGrounded);
    }

    private void Gravity()
    {
        if ((!rigidbody.useGravity))
        {
            rigidbody.useGravity = true;
        }
        rigidbody.AddForce(Vector3.up * gravity);
    }

    private void WaveGravity()
    {
        rigidbody.AddForce(Vector3.up * waveGravity);
    }

    private void Dash()
    {
        if (Input.GetButton("Jump") && isDashEnabled)
        {
            if (facingRight)
            {
                Vector3 dashTarget = new Vector3(transform.position.x + dashValue, transform.position.y);
                transform.DOMove(dashTarget,0.5f);
            }
            else
            {
                Vector3 dashTarget = new Vector3(transform.position.x - dashValue, transform.position.y);
                transform.DOMove(dashTarget,0.5f);  
            }    
            isDashEnabled = false;
            playerState = PlayerStates.Walking;
        }
    }

    public void setDashDirection(Vector3 dashTarget)
    {
        
    }
    private void OnCollisionEnter(Collision _)
    {
        playerState = PlayerStates.Walking;
    }

    private void OnTriggerExit(Collider _)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void Death()
    {
        
    }
}