using System.Collections;
using UnityEngine;

public class FightingState : MonoBehaviour
{

    [SerializeField] private Animator anims;

    [SerializeField] private MovementState ms;

    public PlayerInput playerInput;


    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Attack.performed += ctx => Attack();
        playerInput.Player.Dash.performed += ctx => PlayerManager.Instance.Dash();

        anims.SetBool("isFighting", true);
        anims.SetTrigger("Fight");

        StopCoroutine("Fight");
        StartCoroutine("Fight");
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Player.Attack.performed -= ctx => Attack();
        playerInput.Player.Dash.performed -= ctx => PlayerManager.Instance.Dash();

        anims.SetBool("isFighting", false);
    }

    void Attack()
    {
        StopCoroutine("Fight");
        StartCoroutine("Fight");
    }


    IEnumerator Fight()
    {
        yield return new WaitForSeconds(0.34f);
        Automata.Instance.ChangeState("MovementState");
    }







}
