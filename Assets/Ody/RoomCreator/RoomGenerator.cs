using UnityEngine;

public class RoomCreator2D : MonoBehaviour
{
    public GameObject wallPrefab;  // Wall prefab for the outline (outer walls)
    public GameObject interiorPrefab; // Interior prefab (e.g., floor or other objects inside)

    public int roomWidth = 10;     // Width of the room
    public int roomHeight = 5;     // Height of the room

    void Start()
    {
        CreateRoom();
    }

    // Function to create the room (walls and interior)
    void CreateRoom()
    {
        // Create the walls (outline of the box)
        CreateWalls();

        // Create the interior (inside the box, filling the area)
        CreateInterior();
    }

    // Function to create the walls (outline)
    void CreateWalls()
    {
        // Create the walls on the top and bottom
        for (int x = 0; x < roomWidth; x++)
        {
            // Top wall
            Instantiate(wallPrefab, new Vector3(x, roomHeight - 1, 0), Quaternion.identity);

            // Bottom wall
            Instantiate(wallPrefab, new Vector3(x, 0, 0), Quaternion.identity);
        }

        // Create the walls on the left and right sides
        for (int y = 0; y < roomHeight; y++)
        {
            // Left wall
            Instantiate(wallPrefab, new Vector3(0, y, 0), Quaternion.identity);

            // Right wall
            Instantiate(wallPrefab, new Vector3(roomWidth - 1, y, 0), Quaternion.identity);
        }
    }

    // Function to create the interior (fill the inside of the box)
    void CreateInterior()
    {
        for (int x = 1; x < roomWidth - 1; x++) // Exclude the outer walls
        {
            for (int y = 1; y < roomHeight - 1; y++) // Exclude the outer walls
            {
                // Place the interior objects inside the box
                Instantiate(interiorPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
