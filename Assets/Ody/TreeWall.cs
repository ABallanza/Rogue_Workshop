using UnityEngine;

public class TreeWall : MonoBehaviour
{


    public Collider col;


    private void Update()
    {
        col.enabled = !PlayerManager.Instance.isGrounded;
    }


}
