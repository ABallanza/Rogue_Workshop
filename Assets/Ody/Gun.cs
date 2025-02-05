using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    public GameObject shootRot;
    public PlayerInput playerInput;

    Vector2 keyInput;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.LockMovement.performed += ctx => LockMove(1);
        playerInput.Player.LockMovement.canceled += ctx => LockMove(2);

        playerInput.Player.Shoot.performed += ctx => Shoot();
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Player.LockMovement.performed -= ctx => LockMove(1);
        playerInput.Player.LockMovement.canceled -= ctx => LockMove(2);

        playerInput.Player.Shoot.performed -= ctx => Shoot();
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
    }

    public void Shoot()
    {
        Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
    }
}
