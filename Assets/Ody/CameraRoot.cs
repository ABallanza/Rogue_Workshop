using UnityEngine;
using UnityEngine.Rendering;

public class CameraRoot : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float sens;


    [SerializeField] private Animator anims;


    public static CameraRoot Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShakeCam()
    {
        anims.Play("Shake");
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, sens / 100);
    }
}
