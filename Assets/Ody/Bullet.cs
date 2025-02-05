using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 10f;

    void Update()
    {
        rb.linearVelocity = transform.forward * speed;    
    }
}
