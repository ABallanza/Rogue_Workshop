using UnityEngine;

public class Link
{
    public ChunkManager chunk1;
    public ChunkManager chunk2;

    public string chunk1Side;
    public string chunk2Side;

    public bool deactivated = false;

    public ChunkManager GetOther(ChunkManager you)
    {
        return you == chunk1 ? chunk2 : chunk1;
    }

    public void ChangeActivation(bool activate)
    {
        deactivated = !activate;
        chunk1.openSides[chunk1Side] = activate;
        chunk2.openSides[chunk2Side] = activate;
    }
}
