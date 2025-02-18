using UnityEngine;

public class Spike : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            Automata.Instance.ChangeState("DamageState");
            other.GetComponent<Rigidbody>().linearVelocity = Vector3.up * 15;
            PlayerManager.Instance.TakeDamage(1);
        }
    }

}
