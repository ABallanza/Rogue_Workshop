using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Android.AndroidGame;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyAutomata automata;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform model;

    [SerializeField] private Transform frontObj;

    public Animator anims;

    public float speed = 5;

    bool hasRotate = false;
    int rot = 0;

    private void FixedUpdate()
    {
        CheckForward();

        model.rotation = Quaternion.Euler(0, rot, 0);
    }


    public bool canGo;
    public LayerMask ground;

    private void Update()
    {
        canGo = Physics.CheckSphere(frontObj.position, 0.1f, ground);

        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < 15)
        {
            if(Mathf.Abs(transform.position.y - GameObject.Find("Player").transform.position.y) < 1)
            {
                automata.ChangeState("FightingState");
            }
        }

        anims.SetBool("isMoving", rb.linearVelocity.magnitude > 0.2f);
    }



    public void CheckForward()
    {
        if (CanGoForward() && !canGo)
        {
            rb.linearVelocity = model.right * - speed / 5;
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

        Vector3 RightAndABit = model.position + -model.right * 1.2f;

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
