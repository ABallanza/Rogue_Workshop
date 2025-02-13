using System.Collections;
using UnityEngine;

public class WalljumpState : MonoBehaviour
{


    public PlayerInput playerInput;

    public MovementState ms;


    private void OnEnable()
    {
        PlayerManager.Instance.canRotate = false;

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
        PlayerManager.Instance.canRotate = true;
        PlayerManager.Instance.GetComponent<Collider>().enabled = false;
        PlayerManager.Instance.rb.isKinematic = false;
        PlayerManager.Instance.rb.linearVelocity = PlayerManager.Instance.model.right * -5f;
        PlayerManager.Instance.rb.linearVelocity = new Vector3(PlayerManager.Instance.rb.linearVelocity.x, 15f ,PlayerManager.Instance.rb.linearVelocity.z);
        StartCoroutine(EndJump());
    }

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerManager.Instance.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        Automata.Instance.ChangeState("MovementState");
        
    }


}
