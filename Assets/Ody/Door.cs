using UnityEngine;

public class Door : MonoBehaviour
{


    public PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Interagir.performed += ctx => EnterDoor();
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Player.Interagir.performed -= ctx => EnterDoor();
    }


    public bool upDoor = false;
    public bool downDoor = false;
    public bool leftDoor = false;
    public bool rightDoor = false;

    private bool isIn = false;


    void EnterDoor()
    {
        if (isIn)
        {
            print("RENTRER");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isIn = false;
        }
    }


}
