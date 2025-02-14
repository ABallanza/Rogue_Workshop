using System.Security.Cryptography;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    


    public void Push(float force)
    {
        rb.linearVelocity = PlayerManager.Instance.model.right * force;
    }

    public void Attack(GameObject GO)
    {
        Instantiate(GO, transform.position, transform.rotation);
    }


}
