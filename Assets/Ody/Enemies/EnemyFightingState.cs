using UnityEngine;

public class EnemyFightingState : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Rotate the enemy to face the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0; // Keep rotation horizontal

        if (directionToPlayer != Vector3.zero)
        {
            model.rotation = Quaternion.LookRotation(directionToPlayer);
        }

        if (CanGoForward())
        {
            rb.linearVelocity = directionToPlayer * 5f;
        }
        else
        {
            rb.linearVelocity = Vector3.zero; // Stop if unable to move forward
        }
    }

    bool CanGoForward()
    {
        RaycastHit hit;
        Vector3 RightAndABit = model.position + -model.right * 2f;

        if (Physics.Raycast(RightAndABit, Vector3.down, out hit, 2f))
        {
            return hit.transform != null;
        }
        return false;
    }
}
