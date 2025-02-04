using UnityEngine;

public class Wall : MonoBehaviour
{

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider hitCollider in hitColliders)
        {
            if(hitCollider.tag == "Background")
            {
                Destroy(gameObject);
            }
        }
    }
}