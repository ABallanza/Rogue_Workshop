using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [Header("Variales")]
    public Rigidbody rb;

    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Ground Settings")]
    public bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CheckGround();
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
