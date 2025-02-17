using System.Security.Cryptography;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool ground;
    public bool groundDist;
    public bool aerial;


    public void Start()
    {
        if(Generator.Instance.actualEnemiesNumber != 0)
        {
            Generator.Instance.actualEnemiesNumber--;
            if (ground)
            {
                GameObject o = Instantiate(Generator.Instance.groundMelee, transform.position, Quaternion.identity);
                o.transform.SetParent(transform);
            }
            if (groundDist)
            {
                GameObject o = Instantiate(Generator.Instance.groundDistance, transform.position, Quaternion.identity);
                o.transform.SetParent(transform);
            }
            if (aerial)
            {
                GameObject o = Instantiate(Generator.Instance.Aerial, transform.position, Quaternion.identity);
                o.transform.SetParent(transform);
            }
        }
    }
}
