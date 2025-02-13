using System.Collections;
using UnityEngine;

public class DashState : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public Collider col;


    private void OnEnable()
    {
        PlayerManager.Instance.GetComponent<Collider>().excludeLayers = LayerMask.GetMask("Enemy");
        rb.linearVelocity = PlayerManager.Instance.model.right * PlayerManager.Instance.dashForce * 10;
        StartCoroutine("EndDash");
    }


    IEnumerator EndDash()
    {
        yield return new WaitForSeconds(PlayerManager.Instance.dashTime);
        rb.linearVelocity = Vector3.zero;
        PlayerManager.Instance.GetComponent<Collider>().excludeLayers = LayerMask.GetMask("GroundPassable");
        Automata.Instance.ChangeState("MovementState");
    }
}
