using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour
{
    public GameObject text;

    public PlayerInput playerInput;

    public bool canOpen = false;

    public bool openned = false;

    public GameObject shopCanvas;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Interagir.performed += ctx => OpenChest();
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.Interagir.performed -= ctx => OpenChest();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !openned)
        {
            text.SetActive(true);
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            text.SetActive(false);
            canOpen = false;
        }
    }

    public void OpenChest()
    {
        print("TEst");
        if(!openned && canOpen)
        {
            openned = true;
            text.SetActive(false);
            shopCanvas.SetActive(true);
        }
    }
}
