using System;
using DG.Tweening;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public enum PlayerStates
    {
        Walking,
        Dashing
    }
    
    [Header("Basic Movement")] 
    
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 0.25f;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float gravitySpeed = -9.8f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private bool isGrounded;
    public PlayerStates playerState;
    private bool facingRight = true;

    [Header("Dash")] 
    
    public float dashSpeed;
    public float startDashTime;
    private float dashTime;
    private int direction;
    private bool isDashEnabled;

    private void Start()
    {
        playerState = PlayerStates.Walking;
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        switch (playerState)
        {
            case PlayerStates.Walking:
                GroundCheck();
                Gravity();
                Movement(h);
                Jump();
                Flip(h);                
                break;
            case PlayerStates.Dashing:
                rigidbody.velocity = Vector3.zero;
                rigidbody.useGravity = false;
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
        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.3f, ground);
    }

    private void Gravity()
    {
        if (!rigidbody.useGravity)
        {
            rigidbody.useGravity = true;
        }
        rigidbody.AddForce(Vector3.up * gravitySpeed);
    }

    private void Dash(float h)
    {
        if (isDashEnabled)
        {
            
        }
    }
}