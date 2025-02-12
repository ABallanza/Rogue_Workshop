using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Android.AndroidGame;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyAutomata automata;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform model;

    bool hasRotate = false;
    int rot = 0;

    private void FixedUpdate()
    {
        CheckForward();

        model.rotation = Quaternion.Euler(0, rot, 0);
    }


    private void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < 15)
        {
            if(Mathf.Abs(transform.position.y - GameObject.Find("Player").transform.position.y) < 1)
            {
                automata.ChangeState("FightingState");
            }
        }
    }



    public void CheckForward()
    {
        if (CanGoForward())
        {
            rb.linearVelocity = model.right * -5;
        }
        else
        {
            if (hasRotate)
            {
                hasRotate = false;
                rot = 180;
                return;
            }
            else
            {
                hasRotate = true;
                rot = 0;
                return;
            }
        }
    }


    bool CanGoForward()
    {
        RaycastHit hit;

        Vector3 RightAndABit = model.position + -model.right * 2f;

        if (Physics.Raycast(RightAndABit, Vector3.down, out hit, 2f))
        {
            if(hit.transform != null)
            {
                return true;
            }
        }
        return false;
    }

}
