using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Collider col;

    private Transform player;


    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        col.enabled = player.position.y > transform.position.y;
    }
}
