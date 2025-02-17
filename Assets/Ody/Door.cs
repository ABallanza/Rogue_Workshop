using System.Collections.Generic;
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




    private bool isIn = false;

    public Dictionary<string, bool> doorDict = new Dictionary<string, bool>() { ["Up"] = false, ["Down"] = false, ["Left"] = false, ["Right"] = false };

    private void Start()
    {
        print("door" + doorDict["Left"]);
    }

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
