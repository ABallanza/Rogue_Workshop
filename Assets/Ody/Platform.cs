using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Collider col;

    private Transform player;


    [Header("Layers")]
    public LayerMask defaultLayer;
    public LayerMask ingoreLayer;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

}
