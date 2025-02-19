using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 10f;

    [SerializeField] private GameObject impact;

    public bool isPlayer = false;

    public bool isPower = false;
    public GameObject power;

    void Update()
    {
        rb.linearVelocity = transform.forward * speed;    
    }


    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impact, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer)
        {
            if (other.tag == "Enemy")
            {
                if (isPower)
                {
                    Instantiate(power, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }

}
