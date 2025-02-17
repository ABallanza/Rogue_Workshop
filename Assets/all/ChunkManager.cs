using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class ChunkManager : MonoBehaviour
{
    public Dictionary<string, bool> openSides = new Dictionary<string, bool>() { ["Up"] = false, ["Down"] = false, ["LeftUp"] = false, ["LeftDown"] = false, ["RightUp"] = false, ["RightDown"] = false};
    public Dictionary<string, Link> neighbours = new Dictionary<string, Link>();

    public bool isOffset;

    public List<bool> debug = new List<bool>();

    private void Update()
    {
        debug = openSides.Values.ToList();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (Link i in neighbours.Values)
        {
            if (!i.deactivated)
            {
                Gizmos.DrawLine(i.chunk1.gameObject.transform.position + new Vector3(0, 0, -1), i.chunk2.gameObject.transform.position + new Vector3(0, 0, -1));
            }
        }
    }


    public List<GameObject> roomsAvailable = new List<GameObject>();

    public void SpawnChunk(List<GameObject> blacklist = null, bool isDoor = false, string doorKey = "")
    {
        Generator gen = Generator.Instance;
        roomsAvailable.Clear();

        if (!isDoor)
        {
            foreach (string key in gen.roomsDictionary.Keys)
            {
                if (openSides[key])
                {
                    roomsAvailable = roomsAvailable.Union(gen.roomsDictionary[key]).ToList();
                }
            
            }

            if (blacklist != null)
            {
                foreach (GameObject gameObject in blacklist)
                {
                    if (roomsAvailable.Contains(gameObject))
                    {
                        roomsAvailable.Remove(gameObject);
                    }
                }
            }
            else
            {
                blacklist = new List<GameObject>();
            }

            GameObject newRoom = roomsAvailable[Random.Range(0, roomsAvailable.Count)];

            bool doesntFit = !gen.AddChunk(newRoom.GetComponent<Room>(), gen.Chunks.IndexOf(this));

            if (doesntFit)
            {
                blacklist.Add(newRoom);
                SpawnChunk(blacklist);
            }
            else
            {
                GameObject r = Instantiate(newRoom, transform.position, Quaternion.identity);
                r.transform.SetParent(transform);
            }
        }
        else
        {
            GameObject newRoom = gen.doorRooms[Random.Range(0, gen.doorRooms.Length)];
            GameObject door = Instantiate(newRoom, transform.position, Quaternion.identity);
            door.GetComponentInChildren<Door>().doorDict[doorKey] = true;
            door.transform.SetParent(transform);
        }
        

    }

    
}
