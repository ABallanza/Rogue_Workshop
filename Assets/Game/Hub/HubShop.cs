using UnityEngine;

public class HubShop : MonoBehaviour
{

    public GameObject shopCanvas;


    public void OnTriggerEnter(Collider other)
    {
        shopCanvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        shopCanvas.SetActive(false);
    }


}
