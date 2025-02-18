using System.Collections;
using UnityEngine;

public class EnemyFightingState : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private EnemyAutomata automata;

    [Header("Fight Settings")]
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float timeBetweenHits = 1f;
    [SerializeField] private Animator anims;
    [SerializeField] private float moveSpeed = 5f;

    private bool hasHit = false;

    private void Start()
    {
        player = GameObject.Find("Player")?.transform;
    }

    private void OnEnable()
    {
        hasHit = false;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Flip enemy to face player
        model.rotation = directionToPlayer.x > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);

        // Move towards the player continuously
        rb.linearVelocity = new Vector3(directionToPlayer.x * moveSpeed, rb.linearVelocity.y, 0);

        // Attack when within attack distance
        if (Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            if (!hasHit)
            {
                StartCoroutine(Fight());
            }
        }

        // If enemy is too far from the player vertically, switch states
        if (Mathf.Abs(transform.position.y - player.position.y) > 1)
        {
            automata.ChangeState("MovementState");
        }
    }

    IEnumerator Fight()
    {
        hasHit = true;
        anims.Play("Hit");
        yield return new WaitForSeconds(timeBetweenHits);
        hasHit = false;
    }
}
