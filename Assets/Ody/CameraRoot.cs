using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float sens;



    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, sens / 100);
    }
}
