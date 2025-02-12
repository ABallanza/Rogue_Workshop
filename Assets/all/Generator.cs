using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Properties;
using UnityEditor;
using UnityEngine.Rendering;

public class Generator : MonoBehaviour
{

    public static Generator Instance;

    public GameObject baseChunk;

    public List<ChunkManager> Chunks;

    [Header("Columns Settings")]
    public int columns = 8;
    public int chunksPerColumns = 4;
    public int spacing = 16;


    [Header("Controlled Random")]
    private int randomSpacing;
    public bool isOn = true;
    private int rdmSpacing;
    private int lastColumnOffset;

    private Vector3 pos;

    [Header("Chunks")]
    public GameObject[] rooms;
    public GameObject[] doorRooms;



    [Header("RoomsList")]
    public Dictionary<string, List<GameObject>> roomsDictionary = new Dictionary<string, List<GameObject>>();

    [Header("Door Settings")]
    public List<int> doorIndexes = new List<int>();
    public int totalRoomsIndex;


    private void Awake()
    {
        Instance = this;
        CreateLists();
        totalRoomsIndex = columns * chunksPerColumns;
        doorIndexes.Add(Random.Range(0, chunksPerColumns));
        doorIndexes.Add(Random.Range((totalRoomsIndex-chunksPerColumns), totalRoomsIndex));
        doorIndexes.Add(chunksPerColumns * (Random.Range(1, columns-1)));
        doorIndexes.Add(chunksPerColumns * (Random.Range(2, columns)) - 1);

    }


    public void Start()
    {
        GenerateChunks();

    }

    void GenerateChunks()
    {
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
                lastColumnOffset = Random.Range(-3, 4);
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
            if (doorIndexes.Contains(v))
            {
                chunk.SpawnChunk(null, true);
            }
            else
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
            print(Chunks[chunkID].openSides[key] + " " + key + " to " + roomToAdd.openSides[key]);

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
        foreach (string key in new List<string>() { "Up", "Down", "LeftUp", "LeftDown", "RightUp", "RightDown" })
        {
            roomsDictionary.Add(key, new List<GameObject>());
        }

        foreach (GameObject r in rooms)
        {
            foreach (string key in roomsDictionary.Keys)
            {
                roomsDictionary[key].Add(r);
            }
        }
    }
}
