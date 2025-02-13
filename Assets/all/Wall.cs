using UnityEngine;

public class Wall : MonoBehaviour
{

    int i = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

}
