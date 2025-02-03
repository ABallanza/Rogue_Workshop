using UnityEngine;

public class MovementState : MonoBehaviour
{
    public PlayerInput playerInput;

    [Header("Rotation")]
    [SerializeField] private bool goingLeft;
    [SerializeField] private float rotationSpeed = 0.5f;

    private Vector2 movInput;
    private Rigidbody rb;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Jump.performed += ctx => Jump();
        playerInput.Player.Jump.canceled += ctx => EndJump();
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Player.Jump.performed -= ctx => Jump();
        playerInput.Player.Jump.canceled -= ctx => EndJump();
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

    void EndJump()
    {
        if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        }
    }

    private void Update()
    {
        movInput = playerInput.Player.Move.ReadValue<Vector2>() * PlayerManager.Instance.speed;
        if(movInput.x < 0)
        {
            goingLeft = true;
        }
        else if(movInput.x > 0)
        {
            goingLeft = false;
        }

        RotateModel();
    }

    void RotateModel()
    {
        Quaternion targetRotation = goingLeft
            ? Quaternion.Euler(0, 180, 0) // Face left
            : Quaternion.Euler(0, 0, 0);  // Face right

        PlayerManager.Instance.model.rotation = Quaternion.Lerp(
            PlayerManager.Instance.model.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (rb)
        {
            rb.linearVelocity = new Vector3(movInput.x, rb.linearVelocity.y, movInput.y);
        }
    }
}
