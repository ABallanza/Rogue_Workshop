using System.Collections;
using UnityEngine;

public class OutsideDoor : MonoBehaviour
{

    public Transform startPos;

    public PlayerInput playerInput;

    bool isIn = false;

    public bool tpPlayer = true;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Interagir.performed += ctx => Leave();
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.Interagir.performed -= ctx => Leave();
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

    void Leave()
    {
        if (isIn)
        {
            Generator.Instance.StartCoroutine("PlaceChunks", 1);
        }
    }



    public void Start()
    {
        StartCoroutine(TeleportPlayer());
    }

    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(1f);
        if (tpPlayer)
        {
            GameObject.Find("Player").GetComponent<Rigidbody>().position = startPos.position;
        }
    }
}
