using System.Collections;
using UnityEngine;

public class EnemyFlyFightingState : MonoBehaviour
{

    public EnemyAutomata automata;

    public float timeBetweenHits;

    public Rigidbody rb;

    public Animator anims;

    public float distanceToStopFight;

    private void OnEnable()
    {
        StartCoroutine(StartFight());
    }
    
    public void FixedUpdate()
    {
        rb.linearVelocity = (GameObject.Find("Player").transform.position - transform.position).normalized * 8;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > distanceToStopFight)
        {
            automata.ChangeState("MovementState");
        }
    }

    IEnumerator StartFight()
    {
        yield return new WaitForSeconds(timeBetweenHits);
        anims.Play("Hit");
        StartCoroutine(StartFight());
    }

}
