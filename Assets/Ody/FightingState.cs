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

        anims.SetBool("isFighting", true);
        anims.SetTrigger("Fight");

        goingLeft = ms.goingLeft;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Player.Attack.performed -= ctx => Attack();

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

    Vector2 movInput;
    bool goingLeft;
    float rotationSpeed = 50f;

    private void Update()
    {
        movInput = playerInput.Player.Move.ReadValue<Vector2>() * PlayerManager.Instance.speed;
        if (movInput.x < 0)
        {
            goingLeft = true;
        }
        else if (movInput.x > 0)
        {
            goingLeft = false;
        }

        RotateModel();
    }

    void RotateModel()
    {
        Quaternion targetRotation = goingLeft
            ? Quaternion.Euler(0, 180, 0) // Face left
            : Quaternion.Euler(0, 0, 0);  // Face right

        PlayerManager.Instance.model.rotation = Quaternion.Lerp(
            PlayerManager.Instance.model.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }





}
