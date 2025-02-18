using TMPro.EditorUtilities;
using UnityEngine;

public class AnimsEvents : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject explosion;


    public void Boom()
    {
        GameObject g = Instantiate(explosion, spawnPoint.transform.position, Quaternion.identity);
        Destroy(g, 10f);
    }
}
