using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Moneyy")]
    public int shards;
    public Text shardsText;
    public Text gemText;

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
    public Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private Collider groundCol;

    [Header("Life Settings")]
    [SerializeField] private int life;
    [SerializeField] private GameObject heart;
    [SerializeField] private Transform lifeHolder;
    private List<GameObject> heartList = new List<GameObject>();

    [Header("Stats")]
    public float meleeDamage = 5;
    public float bulletDamage = 5;
    public float dashForce = 10f;

    [Header("Dash")]
    private bool canDash = true;
    public float dashTime = 0.2f;
    public float timeToDashAgain = 3f;
    public Image dashSlider;

    [Header("Gun Settings")]
    public float fireRate = 0.5f;
    public bool canShoot = true;


    [Header("Vault Settings")]
    public Transform vaultPos;

    [Header("Adder")]
    public Transform layout;
    public GameObject shardAdder;
    public GameObject gemAdder;


    public void AddShard(string name, string price, int number)
    {
        shards += number;
        GameObject t = Instantiate(shardAdder, layout);
        t.GetComponent<Shard>().SetInfo(name, price);
    }

    public void AddGem(string name, string price)
    {
        PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 1);
        GameObject t = Instantiate(gemAdder, layout);
        t.GetComponent<Shard>().SetInfo(name, price);
    }

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

    public void Dash()
    {
        if (canDash)
        {
            canDash = false;
            dashSlider.fillAmount = 1; // Set the UI fill amount to full
            Automata.Instance.ChangeState("DashState");
            StartCoroutine(DashCooldown());
        }
    }

    IEnumerator DashCooldown()
    {
        float elapsedTime = 0f;

        while (elapsedTime < timeToDashAgain)
        {
            dashSlider.fillAmount = 1 - (elapsedTime / timeToDashAgain); // Reduce fill amount over time
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dashSlider.fillAmount = 0; // Ensure it's fully depleted
        canDash = true;
    }


    IEnumerator CanDashAgain()
    {
        yield return new WaitForSeconds(timeToDashAgain);
        canDash = true;
    }


    public bool wallJump = false;

    [HideInInspector] public Vector3 dir;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Walljump_Basic")
        {
            dir = model.transform.right;
            rb.linearDamping = 12;
            wallJump = true;
        }


        if (collision.transform.CompareTag("Walljump"))
        {
            Automata.Instance.ChangeState("WalljumpState");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Walljump_Basic")
        {
            StartCoroutine(StopWalljump());
            rb.linearDamping = 3;
        }
    }

    IEnumerator StopWalljump()
    {
        yield return new WaitForSeconds(0.1f);
        wallJump = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox_Enemy")
        {
            Automata.Instance.ChangeState("DamageState");
            rb.linearVelocity = (transform.position - other.transform.position) * 15;
            TakeDamage(1);
        }
    }

    [Header("Death")]
    public GameObject deathCam;
    public GameObject states;
    public GameObject deadCanvas;


    public Animator openclose;

    public void OpenClose()
    {
        openclose.Play("Close");
    }

    bool isDead = false;

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
                if(life <= 0 && !isDead)
                {
                    isDead = true;
                    states.SetActive(false);
                    deathCam.SetActive(true);
                    GetComponentInChildren<Animator>().Play("Death");
                    deadCanvas.SetActive(true);
                    StartCoroutine("Dead");
                }
            }
        }
    }

    public IEnumerator Dead()
    {
        yield return new WaitForSeconds(4f);
        Application.LoadLevel("Hub");
    }

    private void Start()
    {
        AddLife(10 + PlayerPrefs.GetInt("Life"));
    }

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Jump.performed += ctx => goDown = true;
        playerInput.Player.Jump.canceled += ctx => goDown = false;
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

    private bool goDown = false;

    private void Update()
    {
        groundCol.enabled = isGrounded;

        GetComponentInChildren<Animator>().SetBool("isGrounded", isGrounded);

        if (isGrounded && canMove && goDown)
        {
            groundCol.enabled = !(playerInput.Player.Move.ReadValue<Vector2>().y < 0);
        }

        RotateModel();

        Vault();

        shardsText.text = shards.ToString();

        gemText.text = PlayerPrefs.GetInt("Gem").ToString();
    }

    void Vault()
    {
        if (!isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(vaultPos.position, Vector3.down, out hit, 0.5f))
            {
                if (hit.transform != null && hit.transform.CompareTag("Ground"))
                {
                    StartCoroutine(SmoothVault(hit.point));
                }
            }
        }
    }

    IEnumerator SmoothVault(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(targetPosition.x, targetPosition.y + 0.7f, targetPosition.z);
        float elapsedTime = 0f;
        float duration = 0.25f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
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
}

