using System.Collections;
using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    public GameObject door;

    Vector3 pos;

    public Generator gen;


    public void Start()
    {
        StartCoroutine(CheckPositions());
    }

    IEnumerator CheckPositions()
    {
        yield return new WaitForSeconds(0.2f);

        pos = Vector3.left;
        CheckRaycast(pos);

        yield return new WaitForSeconds(0.2f);

        pos = Vector3.right;
        CheckRaycast(pos);

        yield return new WaitForSeconds(0.2f);

        pos = Vector3.up;
        CheckRaycast(pos);

        yield return new WaitForSeconds(0.2f);

        pos = Vector3.down;
        CheckRaycast(pos);
    }

    void CheckRaycast(Vector3 pos)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, pos, out hit))
        {
            if (hit.transform.tag == "Ground")
            {
                GameObject d = Instantiate(door, hit.point, Quaternion.identity);
                if(pos == Vector3.left)
                {
                    d.GetComponent<Door>().doorType = Door.DoorType.Left;
                    gen.leftDoor = d;
                }
                if (pos == Vector3.right)
                {
                    d.GetComponent<Door>().doorType = Door.DoorType.Right;
                    gen.rightDoor = d;
                }
                if (pos == Vector3.up)
                {
                    d.GetComponent<Door>().doorType = Door.DoorType.Up;
                    gen.upDoor = d;
                }
                if (pos == Vector3.down)
                {
                    d.GetComponent<Door>().doorType = Door.DoorType.Down;
                    gen.downDoor = d;
                }
                d.GetComponent<Door>().generator = gen.gameObject;
                d.transform.SetParent(gen.transform);
            }
        }
    }
}
