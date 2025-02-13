using UnityEngine;

public class OutlineMaker : MonoBehaviour
{
    public bool checkLeft = false;
    public bool checkRight = false;
    public bool checkUp = false;
    public bool checkDown = false;

    public float rayDistance = 1f; // Distance of raycasts
    public LayerMask detectionLayer; // Layer to check for collisions

    private void Start()
    {
        CheckSurroundings();
    }

    void CheckSurroundings()
    {
        if (checkLeft && RaycastDirection(Vector3.left)) return;
        if (checkRight && RaycastDirection(Vector3.right)) return;
        if (checkUp && RaycastDirection(Vector3.up)) return;
        if (checkDown && RaycastDirection(Vector3.down)) return;
    }

    bool RaycastDirection(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, rayDistance, detectionLayer))
        {
            Debug.Log($"Hit detected in {direction} direction. Destroying {gameObject.name}");
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (checkLeft) Gizmos.DrawRay(transform.position, Vector3.left * rayDistance);
        if (checkRight) Gizmos.DrawRay(transform.position, Vector3.right * rayDistance);
        if (checkUp) Gizmos.DrawRay(transform.position, Vector3.up * rayDistance);
        if (checkDown) Gizmos.DrawRay(transform.position, Vector3.down * rayDistance);
    }
}
