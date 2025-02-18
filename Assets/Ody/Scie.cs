using UnityEngine;
using UnityEngine.Rendering;

public class Scie : MonoBehaviour
{

    [SerializeField] private bool clockWise;

    [SerializeField] private float speeeeeeeeeeed;

    float f = 0;

    private void FixedUpdate()
    {
        if(clockWise)
        {
            f -= speeeeeeeeeeed / 100;
            return;
        }

        f += speeeeeeeeeeed / 100;
    }


    private void Update()
    {
        transform.rotation = Quaternion.EulerRotation(0, 0, f);
    }

}
