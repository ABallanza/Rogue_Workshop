using UnityEngine;
using UnityEngine.Rendering;

public class MovementState : MonoBehaviour
{

    public PlayerInput playerInput;

    [Header("Hide")]
    private Vector2 movInput;
    private Rigidbody rb;


    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Jump.performed += ctx => Jump();
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Player.Jump.performed -= ctx => Jump();
    }

    private void Start()
    {
        rb = PlayerManager.Instance.rb;
    }



    void Jump()
    {
        if (PlayerManager.Instance.isGrounded)
        {
            rb.linearVelocity = Vector3.up * PlayerManager.Instance.jumpForce;
        }
    }


    private void Update()
    {
        movInput = playerInput.Player.Move.ReadValue<Vector2>() * PlayerManager.Instance.speed;
    }

    private void FixedUpdate()
    {
        if (rb) { rb.linearVelocity = new Vector3(movInput.x, rb.linearVelocity.y, movInput.y); }
    }




}
