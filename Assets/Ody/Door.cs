using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Text doorText;

    public bool chunks;
    public bool shop;
    public bool challenge;


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
            doorText.text = "Challenge Room";
            if(Generator.Instance.roomsBeforeBoss <= 1)
            {
                doorText.text = "Boss Room";
                doorText.color = Color.red;
            }
            challenge = true;
            Generator.Instance.z++;
            return;
        }
        if(Generator.Instance.z == 1)
        {
            doorText.text = "Combat Room";
            if (Generator.Instance.roomsBeforeBoss <= 1)
            {
                doorText.text = "Boss Room";
                doorText.color = Color.red;
            }
            chunks = true;
            Generator.Instance.z++;
            return;
        }
        if(Generator.Instance.z == 2)
        {
            doorText.text = "Shop Room";
            if (Generator.Instance.roomsBeforeBoss <= 1)
            {
                doorText.text = "Boss Room";
                doorText.color = Color.red;
            }
            shop = true;
            Generator.Instance.z++;
            return;
        }
        if (Generator.Instance.z == 3)
        {
            doorText.text = "Challenge Room";
            if (Generator.Instance.roomsBeforeBoss <= 1)
            {
                doorText.text = "Boss Room";
                doorText.color = Color.red;
            }
            challenge = true;
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
            if (challenge)
            {
                Generator.Instance.StartCoroutine("PlaceChallenge");
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
