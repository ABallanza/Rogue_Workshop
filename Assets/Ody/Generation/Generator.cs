using Autodesk.Fbx;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Room;

    [Header("Player Spawning")]
    [SerializeField] private bool needToSpawnPlayer = false;
    bool spawnedPlayer = false;
    public GameObject Player;
    public GameObject DoorsSpawner;

    public int columns = 5;
    public int roomsPerColumn = 3;
    public int spacing = 15;

    int totalRooms = 0;

    public List<GameObject> rooms;

    Vector3 space;

    [Header("Doors")]
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject upDoor;
    public GameObject downDoor;
    public string doorToTpPlayer;


    [Header("Chunks")]
    public GameObject[] normalChunks;
    public GameObject[] goUpChunks;


    public static Generator Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        StartGeneration();
    }


    public void Reset(string whatDoor)
    {
        doorToTpPlayer = whatDoor;

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);

            Destroy(child.gameObject);
        }

        StartGeneration();
    }


    void StartGeneration()
    {
        totalRooms = columns * roomsPerColumn;

        for(int i = 0; i < columns; i++)
        {
            space.y = Random.Range(-15, 15);
            for (int j = 0; j < roomsPerColumn; j++)
            {
                if(needToSpawnPlayer && !spawnedPlayer)
                {
                    spawnedPlayer = true;
                    Instantiate(Player, space, Quaternion.identity);
                }

                GameObject room = Instantiate(Wall, space, Quaternion.identity);
                room.transform.SetParent(transform);

                

                totalRooms--;

                if(totalRooms == (columns * roomsPerColumn) / 2)
                {
                    GameObject spawner = Instantiate(DoorsSpawner, space, Quaternion.identity);
                    spawner.GetComponent<DoorSpawner>().gen = this;
                    spawner.transform.SetParent(transform);
                }

                rooms.Add(room);
                space.y += spacing;

            }
            space.y = 0;
            space.x += spacing;
        }

        if(doorToTpPlayer != null)
        {
            StartCoroutine("TeleportPlayer");
        }
    }


    public IEnumerator TeleportPlayer()
    {
        GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(1.5f);
        if (doorToTpPlayer == "up")
        {
            downDoor.GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Player").transform.position = downDoor.transform.position;
        }
        if (doorToTpPlayer == "Down")
        {
            upDoor.GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Player").transform.position = upDoor.transform.position;
        }
        if (doorToTpPlayer == "Left")
        {
            rightDoor.GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Player").transform.position = rightDoor.transform.position;
        }
        if (doorToTpPlayer == "Right")
        {
            leftDoor.GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Player").transform.position = leftDoor .transform.position;
        }
        yield return new WaitForSeconds(1f);
        GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = false;
    }

}
