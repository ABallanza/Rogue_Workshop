using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;


    public void Shoot()
    {
        Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
    }
}
