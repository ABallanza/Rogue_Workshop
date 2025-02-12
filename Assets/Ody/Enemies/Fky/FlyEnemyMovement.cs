using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class FlyEnemyMovement : MonoBehaviour
{
    private Vector3 spawnPos;

    public List<Vector3> movePositions;

    private Vector3 gotoPos;

    [SerializeField] private Rigidbody rb;

    public float distanceToFight = 10f;

    public EnemyAutomata automata;

    public void Start()
    {
        spawnPos = transform.position;

        for(int i = 0; i < 5; i++)
        {
            movePositions.Add(new Vector3(spawnPos.x + Random.Range(-8, 8), spawnPos.y = Random.Range(-8, 8), 0f));
        }

        StartCoroutine(StartMoving());
    }



    private void FixedUpdate()
    {
        if(gotoPos != null) 
        {
            rb.linearVelocity = (gotoPos - transform.position).normalized * 2;
        }
    }

    IEnumerator StartMoving()
    {
        if(movePositions.Count > 0)
        {
            gotoPos = movePositions[Random.Range(0, movePositions.Count)];
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(StartMoving());
    }


    private void Update()
    {
        if(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < distanceToFight)
        {
            automata.ChangeState("FightingState");
        }
    }


}
