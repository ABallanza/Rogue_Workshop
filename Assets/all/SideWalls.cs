using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SideWalls : MonoBehaviour
{
    [SerializeField] private Material bunker;
    [SerializeField] private Material jungle;

    [SerializeField] private List<GameObject> recolored;

    private bool isbunker = true;
    private Generator gen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gen = Generator.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gen.isBunker != isbunker)
        {
            isbunker = gen.isBunker;
            foreach (GameObject go in recolored)
            {
                go.GetComponent<MeshRenderer>().material = isbunker ? bunker : jungle;
            }
        }
    }
}
