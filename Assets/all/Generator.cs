using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Properties;
using UnityEditor;
using UnityEngine.Rendering;
using System.Linq;

public class Generator : MonoBehaviour
{
    [HideInInspector] public int z = 0;

    public static Generator Instance;

    public GameObject baseChunk;

    public List<ChunkManager> Chunks;

    public GameObject walls;

    [Header("Player")]
    public bool spawnedPlayer = false;
    public GameObject player;

    [Header("Columns Settings")]
    public int columns = 8;
    public int chunksPerColumns = 4;
    public int spacing = 16;


    [Header("Controlled Random")]
    private int randomSpacing;
    public bool isOn = true;
    private int rdmSpacing;
    private int lastColumnOffset = 0;

    private Vector3 pos;

    [Header("Chunks")]
    public GameObject[] roomsBunker;
    public GameObject[] doorRoomsBunker;

    public GameObject[] roomsJungle;
    public GameObject[] doorRoomsJungle;

    public bool isBunker = true;



    [Header("RoomsList")]
    public Dictionary<string, List<GameObject>> roomsDictionary = new Dictionary<string, List<GameObject>>();

    [Header("Door Settings")]
    public Dictionary<string, int> doorIndexes = new Dictionary<string, int>();
    public int totalRoomsIndex;

    public GameObject outline;

    [Header("Shop")]
    public GameObject shop;

    [Header("End Game")]
    public int roomsBeforeBoss = 5;
    public GameObject bossRoom;


    public bool startGenDirectly = true;

    [Header("Enemies")]
    public GameObject groundMelee;
    public GameObject groundDistance;
    public GameObject Aerial;
    public int enemiesNumber = 30;
    public int actualEnemiesNumber;

    private void Awake()
    {
        Instance = this;
        CreateLists();
        totalRoomsIndex = columns * chunksPerColumns;

        doorIndexes.Add("Left", Random.Range(0, chunksPerColumns));
        doorIndexes.Add("Right", Random.Range((totalRoomsIndex-chunksPerColumns), totalRoomsIndex));
        doorIndexes.Add("Down", chunksPerColumns * (Random.Range(1, columns-1)));
        doorIndexes.Add("Up", chunksPerColumns * (Random.Range(2, columns)) - 1); 

    }



    public Door[] doors;

    void Reset()
    {
        actualEnemiesNumber = 30;
        if (GameObject.Find("Player"))
        {
            PlayerManager.Instance.OpenClose();
        }
        z = 0;
        lastColumnOffset = 0;
        pos = Vector3.zero;
        randomSpacing = 0;
        rdmSpacing = 0;
        CreateLists();
        Chunks.Clear();
        foreach (Transform T in transform)
        {
            if (T != this)
            {
                Destroy(T.gameObject);
            }
        }
    }




    public void Start()
    {
        if (startGenDirectly)
        {
            StartCoroutine(PlaceChunks(1));
        }
    }

    public IEnumerator PlaceChunks(int door)
    {
        roomsBeforeBoss--;

        if(roomsBeforeBoss > 0)
        {
            Reset();
            yield return new WaitForSeconds(1f);
            GenerateChunks();
            SetDoors(door);
        }
        else
        {
            Reset();
            yield return new WaitForSeconds(1f);
            CreateBoss();
        }
    }


    void CreateBoss()
    {
        GameObject g = Instantiate(bossRoom);
        g.transform.SetParent(transform);
    }


    public Transform doorLeft;
    public Transform doorRight;
    public Transform doorDown;
    public Transform doorUp;

    Vector3 tpPos;

    void SetDoors(int i)
    {
        doors = GameObject.FindObjectsOfType<Door>();
        foreach (Door d in doors)
        {
            if (d.doorDict["Left"])
            {
                doorLeft = d.gameObject.transform;
            }

            if (d.doorDict["Right"])
            {
                doorRight = d.gameObject.transform;
            }

            if (d.doorDict["Up"])
            {
                doorUp = d.gameObject.transform;
            }

            if (d.doorDict["Down"])
            {
                doorDown = d.gameObject.transform;
            }
        }

        if(i == 0)
        {
            tpPos = doorRight.position;
            doorRight.GetComponent<Door>().enabled = false;
            doorRight.GetComponent<Door>().doorText.text = "";
            
        }
        if(i == 1)
        {
            tpPos = doorLeft.position;
            doorLeft.GetComponent<Door>().enabled = false;
            doorLeft.GetComponent<Door>().doorText.text = "";
        }
        if(i == 2)
        {
            tpPos = doorDown.position;
            doorDown.GetComponent<Door>().enabled = false;
            doorDown.GetComponent<Door>().doorText.text = "";
        }
        if(i == 3)
        {
            tpPos = doorUp.position;
            doorUp.GetComponent<Door>().enabled = false;
            doorUp.GetComponent<Door>().doorText.text = "";
        }

        if (!spawnedPlayer)
        {
            spawnedPlayer = true;
            Instantiate(player, tpPos, Quaternion.identity);
        }
        else
        {
            GameObject.Find("Player").GetComponent<Rigidbody>().position = tpPos;
        }
    }


    public IEnumerator PlaceShop()
    {
        Reset();
        yield return new WaitForSeconds(1f);
        GenerateShop();
    }

    void GenerateShop()
    {
        GameObject s = Instantiate(shop, Vector3.zero, Quaternion.identity);
        s.transform.SetParent(transform);
        if (!spawnedPlayer)
        {
            spawnedPlayer = true;
            Instantiate(player, Vector3.zero, Quaternion.identity);
        }
        else
        {
            GameObject.Find("Player").transform.position = s.transform.localPosition;
        }
    }

    void GenerateChunks()
    {
        GameObject t = Instantiate(walls, Vector3.zero, Quaternion.identity);
        t.transform.SetParent(transform);

        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < chunksPerColumns; j++)
            {
                GameObject bs = Instantiate(baseChunk, pos, Quaternion.identity);

                bs.transform.SetParent(transform);
                Chunks.Add(bs.GetComponent<ChunkManager>());
                pos.y += spacing;
                CheckVoisins(i * chunksPerColumns + j);
            }

            if (isOn)
            {
                lastColumnOffset += Random.Range(-1, 2);
                rdmSpacing += lastColumnOffset;
                pos.y = (spacing / 2) * rdmSpacing;
            }
            else
            {
                pos.y = 0;
            }

            pos.x += spacing;
        }

        int v = 0;
        foreach (ChunkManager chunk in Chunks)
        {
            bool spawned = false;
            foreach (string key in doorIndexes.Keys)
            {
                if (doorIndexes[key] == v)
                {
                    chunk.SpawnChunk(null, true, key);
                    spawned = true;
                    break;
                }
            }
            
            if (!spawned)
            {
                chunk.SpawnChunk();
            }
            v++;
        }
    }

    void CheckVoisins(int i)
    {
        int colPos = i % chunksPerColumns;

        Chunks[i].isOffset = lastColumnOffset % 2 == 1;

        if (colPos != 0)
        {
            CreateLink(Chunks[i], Chunks[i - 1], "Down", "Up");
        }

        if (i >= chunksPerColumns)
        {
            if(lastColumnOffset + colPos * 2 >= 0 && colPos * 2 + lastColumnOffset < chunksPerColumns * 2)
            {
                int neighbourIndex = i - chunksPerColumns + Mathf.FloorToInt(lastColumnOffset / 2f);

                if (lastColumnOffset % 2 == 0)
                {
                    CreateLink(Chunks[i], Chunks[neighbourIndex], "LeftDown", "RightDown");
                }
                else
                {
                    CreateLink(Chunks[i], Chunks[neighbourIndex], "LeftDown", "RightUp");
                }
            }

            if (lastColumnOffset + colPos * 2 >= -1 && colPos * 2 + lastColumnOffset < chunksPerColumns * 2 - 1)
            {
                int neighbourIndex = i - chunksPerColumns + Mathf.CeilToInt(lastColumnOffset / 2f);

                if (Mathf.Abs(lastColumnOffset) % 2 == 1)
                {
                    CreateLink(Chunks[i], Chunks[neighbourIndex], "LeftUp", "RightDown");
                }
                else
                {
                    CreateLink(Chunks[i], Chunks[neighbourIndex], "LeftUp", "RightUp");
                }
            }
        }
    }

    private void CreateLink(ChunkManager you, ChunkManager other, string yourSide, string otherSide)
    {
        Link link = new Link();
        link.chunk1 = you;
        link.chunk2 = other;
        link.chunk1Side = yourSide;
        link.chunk2Side = otherSide;

        you.neighbours[yourSide] = link;
        other.neighbours[otherSide] = link;

        you.openSides[yourSide] = true;
        other.openSides[otherSide] = true;
    }


    public bool AddChunk(Room roomToAdd, int chunkID, bool isTest = false)
    {

        List<string> changedSides = new List<string>();

        roomToAdd.MakeDict();

        foreach (string key in new List<string>() { "Up", "Down", "LeftUp", "LeftDown", "RightUp", "RightDown" })
        {

            if (!roomToAdd.openSides[key] && Chunks[chunkID].openSides[key])
            {
                Chunks[chunkID].neighbours[key].ChangeActivation(false);
                changedSides.Add(key);
            }
        }

        CheckGraphs(Chunks[chunkID]);

        if(graphCheck.Count < Chunks.Count)
        {
            foreach (string key in changedSides)
            {
                Chunks[chunkID].neighbours[key].ChangeActivation(true);
            }

            return false;
        }

        return true;
    }

    public List<ChunkManager> graphCheck = new List<ChunkManager>();

    public void CheckGraphs(ChunkManager currentChunk, bool firstCheck = true)
    {
        if (firstCheck)
        {
            graphCheck.Clear();
        }

        graphCheck.Add(currentChunk);

        foreach (Link link in currentChunk.neighbours.Values)
        {

            if (!link.deactivated && !graphCheck.Contains(link.GetOther(currentChunk)))
            {
                CheckGraphs(link.GetOther(currentChunk), false);
            }
        }
    }


    void CreateLists()
    {
        roomsDictionary.Clear();

        foreach (string key in new List<string>() { "Up", "Down", "LeftUp", "LeftDown", "RightUp", "RightDown" })
        {
            roomsDictionary.Add(key, new List<GameObject>());
        }

        if (isBunker)
        {
            foreach (GameObject r in roomsBunker)
            {
                foreach (string key in roomsDictionary.Keys)
                {
                    roomsDictionary[key].Add(r);
                }
            }
        }
        else
        {
            foreach (GameObject r in roomsJungle)
            {
                foreach (string key in roomsDictionary.Keys)
                {
                    roomsDictionary[key].Add(r);
                }
            }
        }

        
    }
}
