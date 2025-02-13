using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Android.AndroidGame;
using static UnityEngine.Rendering.DebugUI.Table;

public class EnemyFightingState : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private EnemyAutomata automata;

    [Header("Fight Settings")]
    [SerializeField] private float distanceToHit = 2f;
    private bool hasHit = false;
    [SerializeField] private float timeBetweenHits = 1f;
    [SerializeField] private Animator anims;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        rb.AddForce(model.right * 5);
    }

    void Update()
    {
        if (player == null) return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        if(directionToPlayer.x > 0)
        {
            model.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            model.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (CanGoForward())
        {
            if(Vector3.Distance(transform.position, player.position) > distanceToHit)
            {
                rb.linearVelocity = model.right * -5;
            }
            else
            {
                if (!hasHit)
                {
                    StartCoroutine(Fight());
                }
            }
        }


        if (Mathf.Abs(transform.position.y - GameObject.Find("Player").transform.position.y) > 1)
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
