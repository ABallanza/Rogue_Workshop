using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType { Up, Down, Left, Right }
    public DoorType doorType;


    public GameObject generator;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print(doorType.ToString());
            generator.GetComponent<Generator>().Reset(doorType.ToString());
        }
    }



}
