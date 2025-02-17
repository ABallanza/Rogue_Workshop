using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool chunks;
    public bool shop;


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
        if(Generator.Instance.z == 0)
        {
            chunks = true;
            Generator.Instance.z++;
            return;
        }
        if(Generator.Instance.z == 1)
        {
            shop = true;
            Generator.Instance.z++;
            return;
        }
        if(Generator.Instance.z == 2)
        {
            chunks = true;
            Generator.Instance.z++;
            return;
        }
        if (Generator.Instance.z == 3)
        {
            shop = true;
            Generator.Instance.z++;
            return;
        }
    }

    void EnterDoor()
    {
        if (isIn)
        {
            if (chunks)
            {
                if (doorDict["Left"])
                {
                    if (chunks)
                    {
                        Generator.Instance.StartCoroutine("PlaceChunks", 0);
                    }
                }

                if (doorDict["Right"])
                {
                    if (chunks)
                    {
                        Generator.Instance.StartCoroutine("PlaceChunks", 1);
                    }
                }

                if (doorDict["Up"])
                {
                    if (chunks)
                    {
                        Generator.Instance.StartCoroutine("PlaceChunks", 2);
                    }
                }

                if (doorDict["Down"])
                {
                    if (chunks)
                    {
                        Generator.Instance.StartCoroutine("PlaceChunks", 3);
                    }
                }
            }
            if (shop)
            {
                Generator.Instance.StartCoroutine("PlaceShop");
            }
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
