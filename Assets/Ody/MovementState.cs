using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Player.Jump.performed -= ctx => Jump();
        playerInput.Player.Jump.canceled -= ctx => EndJump();

        playerInput.Player.Attack.performed -= ctx => StartFight();
    }

    private void Start()
    {
        rb = PlayerManager.Instance.rb;
    }

    void StartFight()
    {
        Automata.Instance.ChangeState("FightingState");
    }

    void Jump()
    {
        if (PlayerManager.Instance.isGrounded)
        {
            rb.linearVelocity = Vector3.up * PlayerManager.Instance.jumpForce;
        }
    }





    void EndJump()
    {
        if (rb.linearVelocity.y > 0)
        {
            rvb = rb.linearVelocity.y;
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
            if(rvb < 0.1f)
            {
                rb.linearVelocity = new Vector3(movInput.x, rb.linearVelocity.y, movInput.y);
            }
            else
            {
                rb.linearVelocity = new Vector3(movInput.x, rvb, movInput.y);
            }
        }

        if(rvb > 0)
        {
            rvb -= 1f;
        }
    }
}
