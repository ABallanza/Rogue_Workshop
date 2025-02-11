using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [Header("Vertical Openings")]
    public bool Up = false;
    public bool Down = false;


    [Header("Side Openings")]
    public bool LeftDown = false;
    public bool RightDown = false;
    public bool LeftUp = false;
    public bool RightUp = false;

    public Dictionary<string, bool> openSides = new Dictionary<string, bool>() { ["Up"] = false, ["Down"] = false, ["LeftUp"] = false, ["LeftDown"] = false, ["RightUp"] = false, ["RightDown"] = false };

    public void MakeDict()
    {
        openSides["Up"] = Up;
        openSides["Down"] = Down;
        openSides["LeftUp"] = LeftUp;
        openSides["LeftDown"] = LeftDown;
        openSides["RightUp"] = RightUp;
        openSides["RightDown"] = RightDown;
    }
}
