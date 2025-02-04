using System.Collections;
using UnityEngine;

public class WalljumpState : MonoBehaviour
{


    public PlayerInput playerInput;

    public MovementState ms;


    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Jump.performed += ctx => WJump();

        PlayerManager.Instance.rb.isKinematic = true;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Player.Jump.performed -= ctx => WJump();
    }


    void WJump()
    {
        PlayerManager.Instance.GetComponent<Collider>().enabled = false;
        PlayerManager.Instance.rb.isKinematic = false;
        if (ms.goingLeft)
        {
            PlayerManager.Instance.rb.linearVelocity = Vector3.left * -10f;
        }
        else
        {
            PlayerManager.Instance.rb.linearVelocity = Vector3.left * 10f;
        }
        PlayerManager.Instance.rb.linearVelocity = new Vector3(PlayerManager.Instance.rb.linearVelocity.x, 8f ,PlayerManager.Instance.rb.linearVelocity.z);
        StartCoroutine(EndJump());
    }

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerManager.Instance.GetComponent<Collider>().enabled = true;
        Automata.Instance.ChangeState("MovementState");
        
    }


}
