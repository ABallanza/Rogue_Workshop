using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

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
    [SerializeField] private Collider groundCol;

    [Header("Life Settings")]
    [SerializeField] private int life;
    [SerializeField] private GameObject heart;
    [SerializeField] private Transform lifeHolder;
    private List<GameObject> heartList = new List<GameObject>();

    public void AddLife(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject _heart = Instantiate(heart, lifeHolder);
            _heart.transform.SetParent(lifeHolder);
            if (life % 2 != 0)
            {
                _heart.transform.localScale = new Vector3(-1, 1, 1);
            }
            heartList.Add(_heart);
            life += 1;
        }
    }

    public void TakeDamage(int damage)
    {
        for (int i = 0; i < damage; i++)
        {
            if (life > 0 && heartList.Count > 0)
            {
                GameObject lastHeart = heartList[heartList.Count - 1];
                heartList.RemoveAt(heartList.Count - 1);
                Destroy(lastHeart);
                life -= 1;
            }
        }
    }

    private void Start()
    {
        AddLife(5);
    }

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
        groundCol.enabled = isGrounded;

        if (isGrounded && canMove)
        {
            groundCol.enabled = !(playerInput.Player.Move.ReadValue<Vector2>().y < 0);
        }

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
