using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public PlayerInput playerInput;

    [Header("Variables")]
    public Rigidbody rb;
    public Transform model;
    [HideInInspector] public bool canRotate = true;

    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 5f;
    public bool canMove = true;

    [Header("Ground Settings")]
    public bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.2f;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        RotateModel();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    void CheckGround()
    {
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck transform is not assigned!", this);
            return;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void RotateModel()
    {
        if (!model)
        {
            Debug.LogError("Model transform is not assigned!", this);
            return;
        }

        if (canRotate)
        {
            Vector2 moveInput = playerInput.Player.Move.ReadValue<Vector2>();
            if (moveInput.x != 0)
            {
                model.rotation = Quaternion.Euler(0, moveInput.x > 0 ? 0 : 180, 0);
            }
        }

        if (!isGrounded && rb != null)
        {
            // Rotate based on Rigidbody movement
            float movementDirection = rb.linearVelocity.x;
            if (Mathf.Abs(movementDirection) > 0.1f)
            {
                model.rotation = Quaternion.Euler(0, movementDirection > 0 ? 0 : 180, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Walljump"))
        {
            Automata.Instance.ChangeState("WalljumpState");
        }
    }
}
