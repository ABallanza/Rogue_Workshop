using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 10f;

    [SerializeField] private GameObject impact;


    void Update()
    {
        rb.linearVelocity = transform.forward * speed;    
    }


    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impact, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
