using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform model;

    bool hasRotate = false;
    int rot = 0;

    private void FixedUpdate()
    {
        CheckForward();

        model.rotation = Quaternion.Euler(0, rot, 0);
    }



    public void CheckForward()
    {
        print(CanGoForward());
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
