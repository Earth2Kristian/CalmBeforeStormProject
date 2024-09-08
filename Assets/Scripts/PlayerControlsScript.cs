using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsScript : MonoBehaviour
{
    private static PlayerControlsScript instance = null;
    public InputAction playerControls;

    // Body Variables
    public Rigidbody rb;
    public CapsuleCollider cc;
    public CharacterController controller;

    // Basic Movement Variables
    private Vector2 movementInput = Vector2.zero;
    private Vector3 velocity;
    private float startingSpeed = 12f;
    public float currentSpeed;

    // Basic Jump Variables
    private bool jumped = false;
    public float jumpHeight = 5f;
    public float jumpLimited;
    public bool canDoubleJump;
    public float gravity = -9.8f;

    // Basic Dodge Variables
    private bool dodged = false;
    public bool isDodged = false;
    public float dodgeDistance = 12f;
    public float dodgeTimeLimited = 0.2f;
    //publicAudioSourceswimSoundEffect;

    public Transform playerBody;

    // Ground Check
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    public bool isGrounded;

    private bool paused = false;

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        currentSpeed = startingSpeed;

        jumpLimited = 2f;

        canDoubleJump = true;

        rb.GetComponent<Rigidbody>();
        cc.GetComponent<CapsuleCollider>();
        controller.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // When the player move the left stick left or right
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
        jumped = context.performed;

        if (context.performed && isGrounded)
        {
            // When the player jumps from the ground
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }
    }

    public void OnDoubleJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
        jumped = context.performed;

        if (context.performed && !isGrounded && canDoubleJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canDoubleJump = false;
        }

    }

    public void OnDodgeForward(InputAction.CallbackContext context)
    {
        // When the player select (Square for PS or X button for Xbox)
        dodged = context.action.triggered;
        dodged = context.performed;

        if (context.performed && !isDodged)
        {

            isDodged = true;
            Vector3 dodgeDirection = transform.forward;
            velocity = dodgeDirection * (dodgeDistance / dodgeTimeLimited);
            //swimSoundEffect.Play();
            //GameManager.Instance.dashCurrent -= 1;
            //GameManager.Instance.dashBar.UpdateDashBar(GameManager.Instance.dashCurrent, GameManager.Instance.dashLimited);
            //dashSoundEffect.Play();



            StartCoroutine(Dodge());
        }
    }

    public void OnDodgeLeft(InputAction.CallbackContext context)
    {
        // When the player select (Square for PS or X button for Xbox)
        dodged = context.action.triggered;
        dodged = context.performed;

        if (context.performed && !isDodged)
        {

            isDodged = true;
            Vector3 dodgeDirection = -transform.right;
            velocity = dodgeDirection * (dodgeDistance / dodgeTimeLimited);
            //swimSoundEffect.Play();
            //GameManager.Instance.dashCurrent -= 1;
            // GameManager.Instance.dashBar.UpdateDashBar(GameManager.Instance.dashCurrent, GameManager.Instance.dashLimited);
            //dashSoundEffect.Play();


            StartCoroutine(Dodge());
        }
    }

    public void OnDodgeRight(InputAction.CallbackContext context)
    {
        // When the player select (Square for PS or X button for Xbox)
        dodged = context.action.triggered;
        dodged = context.performed;

        if (context.performed && !isDodged)
        {

            isDodged = true;
            Vector3 dodgeDirection = transform.right;
            velocity = dodgeDirection * (dodgeDistance / dodgeTimeLimited);
            //swimSoundEffect.Play();
            //GameManager.Instance.dashCurrent -= 1;
            //GameManager.Instance.dashBar.UpdateDashBar(GameManager.Instance.dashCurrent, GameManager.Instance.dashLimited);
            // dashSoundEffect.Play();


            StartCoroutine(Dodge());
        }
    }

    public void OnDodgeBackward(InputAction.CallbackContext context)
    {
        // When the player select (Square for PS or X button for Xbox)
        dodged = context.action.triggered;
        dodged = context.performed;

        if (context.performed && !isDodged)
        {

            isDodged = true;
            Vector3 dodgeDirection = -transform.forward;
            velocity = dodgeDirection * (dodgeDistance / dodgeTimeLimited);
            //swimSoundEffect.Play();
            //GameManager.Instance.dashCurrent -= 1;
            //GameManager.Instance.dashBar.UpdateDashBar(GameManager.Instance.dashCurrent, GameManager.Instance.dashLimited);
            //dashSoundEffect.Play();


            StartCoroutine(Dodge());
        }
    }

    public void OnPaused(InputAction.CallbackContext context)
    {
        paused = context.action.triggered;
        paused = context.performed;

        if (context.performed)
        {
            //GameManager.Instance.gamePaused = true;

        }
    }


    void Update()
    {
        // Basic Player Movement
        float moveHorizontal = movementInput.x;
        float moveVertical = movementInput.y;

        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        controller.Move(move * currentSpeed * Time.deltaTime);


        // Checks the ground for the player
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            // If the player is on the ground or on a platform
            velocity.y = 0f;
            canDoubleJump = true;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private IEnumerator Dodge()
    {

        yield return new WaitForSeconds(dodgeTimeLimited);

        velocity = Vector3.zero;
        isDodged = false;
    }

    void Awake()
    {
        instance = this;
    }

    public static PlayerControlsScript Instance
    {
        get
        {
            return instance;
        }
    }
}
