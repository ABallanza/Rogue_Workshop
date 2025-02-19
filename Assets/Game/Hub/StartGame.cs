using UnityEngine;

public class StartGame : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel("NarraTest");
    }

}
