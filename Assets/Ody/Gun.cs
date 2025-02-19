using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public static Gun Instance;

    private void Awake()
    {
        Instance = this;
    }


    public GameObject bullet;
    public Transform shootPoint;
    public GameObject shootRot;
    public PlayerInput playerInput;

    bool isShooting = false;

    Vector2 keyInput;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.LockMovement.performed += ctx => LockMove(1);
        playerInput.Player.LockMovement.canceled += ctx => LockMove(2);

        playerInput.Player.Shoot.performed += ctx => isShooting = true;
        playerInput.Player.Shoot.canceled += ctx => isShooting = false;

        playerInput.Player.Hyperbullet.performed += ctx => PowerShoot();
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Player.LockMovement.performed -= ctx => LockMove(1);
        playerInput.Player.LockMovement.canceled -= ctx => LockMove(2);

        playerInput.Player.Shoot.performed -= ctx => isShooting = true;
        playerInput.Player.Shoot.canceled -= ctx => isShooting = false;

        playerInput.Player.Hyperbullet.performed -= ctx => PowerShoot();
    }


    [Header("PowerBullet")]
    public GameObject p_bullet;
    public Image p_image;
    public float p_value;


    void PowerShoot()
    {
        if(p_value <= 0)
        {
            p_value = 1;
            Instantiate(p_bullet, shootPoint.transform.position, shootPoint.transform.rotation);
        }
    }

    public void Kill()
    {
        p_value -= 0.2f;
        p_image.fillAmount = p_value;
    }

    void LockMove(int i)
    {
        if(i == 1)
        {
            PlayerManager.Instance.canMove = false;
        }
        if(i == 2)
        {
            PlayerManager.Instance.canMove = true;
        }
    }

    public void Update()
    {
        keyInput = playerInput.Player.Move.ReadValue<Vector2>();

        if (keyInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(keyInput.y, keyInput.x) * Mathf.Rad2Deg; // Convert input to an angle
            float snappedAngle = Mathf.Round(angle / 45) * 45; // Snap to nearest 45 degrees
            shootRot.transform.rotation = Quaternion.Euler(0, 0, snappedAngle);
        }

        if (isShooting)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (PlayerManager.Instance.canShoot)
        {
            PlayerManager.Instance.canShoot = false;
            StartCoroutine(Reload());
            Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(PlayerManager.Instance.fireRate);
        PlayerManager.Instance.canShoot = true;
    }
}
