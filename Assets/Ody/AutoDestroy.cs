using UnityEngine;

public class AutoDestroy : MonoBehaviour
{


    [SerializeField] private float timeBeforeDestroy;


    public void Start()
    {
        Destroy(gameObject, timeBeforeDestroy);
    }



}
