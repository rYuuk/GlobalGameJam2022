using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlelr : MonoBehaviour
{
    [Header("Basic Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float gravitySpeed = -9.8f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody rigidbody;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        Gravity();
        Movement();
        Jump();
    }

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            transform.Translate(Vector3.right * (h * speed * 2 * Time.deltaTime));
        }
        else
        {
            transform.Translate(Vector3.right * (h * speed * Time.deltaTime));
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
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.2f, ground);
    }

    private void Gravity()
    {
        rigidbody.AddForce(Vector3.up * gravitySpeed);
    }

    private void Dash()
    {
        
    }
}