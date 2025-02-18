using UnityEngine;
using System.Collections;

public class MovementState : MonoBehaviour
{
    public PlayerInput playerInput;
    private Vector2 movInput;
    private Rigidbody rb;
    [SerializeField] private Animator anims;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float maxSpeed = 10f;
    private bool canMoveTowardsWall = true;
    private Vector3 lastWallJumpDir;
    private float wallJumpCooldown = 0.5f;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Jump.performed += ctx => Jump();
        playerInput.Player.Jump.canceled += ctx => EndJump();

        playerInput.Player.Attack.performed += ctx => StartFight();
        playerInput.Player.Dash.performed += ctx => PlayerManager.Instance.Dash();
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.Jump.performed -= ctx => Jump();
        playerInput.Player.Jump.canceled -= ctx => EndJump();

        playerInput.Player.Attack.performed -= ctx => StartFight();
        playerInput.Player.Dash.performed -= ctx => PlayerManager.Instance.Dash();
    }

    void StartFight()
    {
        Automata.Instance.ChangeState("FightingState");
    }

    private void Start()
    {
        rb = PlayerManager.Instance.rb;
    }

    void Jump()
    {
        if (!PlayerManager.Instance.canMove) return;

        if (PlayerManager.Instance.isGrounded && movInput.y >= 0)
        {
            anims.SetTrigger("Jump");
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z); // Set Y velocity directly
        }
        else if (PlayerManager.Instance.wallJump && !PlayerManager.Instance.isGrounded)
        {
            // Wall Jump
            lastWallJumpDir = PlayerManager.Instance.dir.normalized;
            rb.linearVelocity = new Vector3(-lastWallJumpDir.x * 6f, jumpForce + 5, rb.linearVelocity.z); // Apply controlled wall jump force

            StartCoroutine(WallJumpCooldown());
        }
    }

    private IEnumerator WallJumpCooldown()
    {
        canMoveTowardsWall = false;
        yield return new WaitForSeconds(wallJumpCooldown);
        canMoveTowardsWall = true;
    }

    void EndJump()
    {
        if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f, rb.linearVelocity.z); // Reduce jump height slightly
        }
    }

    private void Update()
    {
        movInput = playerInput.Player.Move.ReadValue<Vector2>();
        anims.SetBool("isWalking", rb.linearVelocity.magnitude > 0.7f);

        moveSpeed = PlayerManager.Instance.speed;
        jumpForce = PlayerManager.Instance.jumpForce;
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.canMove) return;

        Vector2 moveDir = movInput.normalized;
        bool movingTowardsWall = Vector3.Dot(new Vector3(moveDir.x, 0, moveDir.y), lastWallJumpDir) > 0.1f;

        if (!movingTowardsWall || canMoveTowardsWall)
        {
            rb.linearVelocity = new Vector3(movInput.x * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z); // Use velocity directly
        }
    }
}
