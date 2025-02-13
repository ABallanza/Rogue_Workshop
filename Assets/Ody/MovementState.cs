using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;

public class MovementState : MonoBehaviour
{
    public PlayerInput playerInput;

    private Vector2 movInput;

    private Rigidbody rb;

    [SerializeField] private Animator anims;

    public float rvb;

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

    private void Start()
    {
        rb = PlayerManager.Instance.rb;
    }

    void StartFight()
    {
        Automata.Instance.ChangeState("FightingState");
    }

    private bool canMoveTowardsWall = false;
    private Vector3 lastWallJumpDir;
    private float wallJumpCooldown = 0.5f; // Cooldown time

    void Jump()
    {
        rvb = 100;

        if (PlayerManager.Instance.isGrounded && playerInput.Player.Move.ReadValue<Vector2>().y >= 0)
        {
            rb.AddForce(Vector3.up * PlayerManager.Instance.jumpForce);
        }

        if (PlayerManager.Instance.wallJump && !PlayerManager.Instance.isGrounded)
        {
            rb.linearVelocity = Vector3.zero; // Reset all movement before applying wall jump forces
            lastWallJumpDir = PlayerManager.Instance.dir.normalized; // Store last wall jump direction
            rb.AddForce(lastWallJumpDir * -600);
            rb.AddForce(Vector3.up * 1400);

            StartCoroutine(WallJumpCooldown()); // Start cooldown
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
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f, rb.linearVelocity.z); // Reduce upward velocity
        }
    }


    private void Update()
    {

        anims.SetBool("isWalking", rb.linearVelocity.magnitude > 0);

        movInput = playerInput.Player.Move.ReadValue<Vector2>() * PlayerManager.Instance.speed;
    }

    private void FixedUpdate()
    {
        if (rb && PlayerManager.Instance.canMove)
        {
            Vector2 moveDir = movInput.normalized; // Get normalized movement direction
            bool movingTowardsWall = Vector3.Dot(new Vector3(moveDir.x, 0, moveDir.y), lastWallJumpDir) > 0.1f;

            if (!movingTowardsWall || canMoveTowardsWall) // Block movement towards the wall
            {
                if (rb.linearVelocity.magnitude < 10)
                {
                    rb.AddForce(new Vector3(movInput.x, rb.linearVelocity.y, movInput.y));
                }
            }
        }
    }
}
