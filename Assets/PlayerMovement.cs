using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AnimationClip dashAnimation;
    [SerializeField] public float moveSpeed = 12f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    public PlayerInputActions playerControls;

    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashDuration = 0.01f;
    [SerializeField] private float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;
    private CircleCollider2D playerCollider;

    private InputAction move;
    private InputAction fire;
    private InputAction dash;

    private Vector2 moveDirection;

    public BoundaryManager boundaryManager; //public for inspector assignment

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnDisable()
    {
        if (move != null) move.Disable();
        if (fire != null) fire.Disable();
        if (dash != null) dash.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CircleCollider2D>();

        if (rb == null) Debug.LogError("Rigidbody2D is missing on the player.");
        if (animator == null) Debug.LogError("Animator component is missing on the player.");
        if (playerCollider == null) Debug.LogError("CircleCollider2D is missing on the player.");
        else playerCollider.enabled = true;

        move = playerControls.Player.Move;
        move.Enable();
        move.performed += Move;
        move.canceled += Move;

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;

        if (boundaryManager == null)
        {
            Debug.LogError("BoundaryManager is not assigned in the PlayerMovement script.");
        }
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Vector2 targetPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(ClampPosition(targetPosition));
        }
    }

    private Vector2 ClampPosition(Vector2 position) //clamp to prevent out of bounds travel
    {
        if (boundaryManager == null)
        {
            Debug.LogError("BoundaryManager reference is missing. Cannot clamp position.");
            return position;
        }

        float clampedX = Mathf.Clamp(position.x, boundaryManager.minX, boundaryManager.maxX);
        float clampedY = Mathf.Clamp(position.y, boundaryManager.minY, boundaryManager.maxY);
        return new Vector2(clampedX, clampedY);
    }

    private void Move(InputAction.CallbackContext context) //updating values for animator
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
            if (context.canceled)
            {
                animator.SetBool("isWalking", false);
                animator.SetFloat("LastInputX", moveInput.x);
                animator.SetFloat("LastInputY", moveInput.y);
            }
        }
        else
        {
            Debug.LogError("Animator is not assigned.");
        }

        moveInput = context.ReadValue<Vector2>();
        if (animator != null)
        {
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash && moveDirection != Vector2.zero)
        {
            Debug.Log("Dash triggered at time: " + Time.time + " seconds"); //log dash time
            StartCoroutine(PerformDash(moveDirection.normalized));
        }
    }

    private IEnumerator PerformDash(Vector2 direction)
    {
        canDash = false;
        isDashing = true;

        if (dashAnimation == null)
        {
            Debug.LogError("Dash animation clip is missing.");
        }
        else
        {
            float animationLength = dashAnimation.length; //setting values for the dash from input
            float dashSpeedMultiplier = animationLength / dashDuration;
            if (animator != null)
            {
                animator.SetFloat("DashSpeedMultiplier", dashSpeedMultiplier); 
                animator.SetFloat("LastInputX", direction.x);
                animator.SetFloat("LastInputY", direction.y);
                animator.SetTrigger("isDashing");
            }
            else
            {
                Debug.LogError("Animator is missing, cannot set dash animation.");
            }
        }

        if (rb != null)
        {
            rb.velocity = direction * dashSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody2D is missing, cannot perform dash.");
        }

        if (playerCollider != null) playerCollider.enabled = false;

        move.Disable(); //disable other movemnt during dash
        dash.Disable();

        yield return new WaitForSeconds(dashDuration);

        if (playerCollider != null) playerCollider.enabled = true;

        if (rb != null) rb.position = ClampPosition(rb.position);

        rb.velocity = Vector2.zero;
        isDashing = false;

        move.Enable();
        dash.Enable();
        fire.Enable();

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        //no logic here for now
    }
}
