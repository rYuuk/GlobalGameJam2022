using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerLight))]
public class CharacterController : MonoBehaviour
{
    public enum PlayerStates
    {
        Walking,
        Reflecting,
        Dashing,
        Wave,
        Dead
    }

    [Header("Basic Movement")] [SerializeField]
    private float speed;

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
    [SerializeField] private Material waveMaterial;
    private int direction;
    public float dashValue;
    public float dashTime;
    private bool isDashEnabled;
    public Transform dashTarget;

    [Header("Animation")] 
    [SerializeField] private Animator playerAnimator;

    private PlayerLight playerLight;
    private bool isJumping = false;


    private void Awake()
    {
        playerLight = GetComponent<PlayerLight>();
        playerState = PlayerStates.Walking;
    }

    private void OnEnable()
    {
        playerLight.LightConsumed += Death;
    }

    private void OnDisable()
    {
        playerLight.LightConsumed -= Death;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        GroundCheck();
        var h = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("speed", h);
        switch (playerState)
        {
            case PlayerStates.Walking:
                Gravity();
                Movement(h);
                Jump();
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
            case PlayerStates.Dead:
                break;
            case PlayerStates.Dashing:
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

    private void Jump() //Called from Animation Event 
    {
        if ((Input.GetButton("Jump")))
        {
            if (isGrounded && !isJumping)
            {
                isJumping = true;
                playerAnimator.SetTrigger("jump");
                Invoke(nameof(ActualJump), 0.02f);
            }
        }
    }

    private void ActualJump()
    {
        rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.3f, ground);
        playerAnimator.SetBool("isGrounded", isGrounded);
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
            var dashVector = transform.position + (dashTarget.right *  dashValue);
            transform.DOMove(dashVector, dashTime);
            waveMaterial.SetFloat("_WaveSpeed",10f);
            waveMaterial.SetFloat("_WaveIntensity",0.2f);
            waveMaterial.SetFloat("_WaveRate",10f);
            StartCoroutine(DashReset());
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (IsInLayerMask(col.gameObject, ground))
        {
            isJumping = false;
        }
        playerState = PlayerStates.Walking;
        transform.DOKill(true);
        if (playerState == PlayerStates.Wave)
        {
            dashTime = 0.1f;
            StartCoroutine(DashReset());
        }
    }
    
    public bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }

    private IEnumerator DashReset()
    {
        yield return new WaitForSeconds(dashTime);
        isDashEnabled = false;
        playerState = PlayerStates.Walking;
        transform.localScale = new Vector3(1, 1, 1);
        waveMaterial.SetFloat("_WaveSpeed", 5);
        waveMaterial.SetFloat("_WaveIntensity", 0.01f);
        waveMaterial.SetFloat("_WaveRate", 4f);
    }

    private void Death()
    {
        playerState = PlayerStates.Dead;
        playerAnimator.SetTrigger("Death");
    }
}