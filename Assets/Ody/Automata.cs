using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automata : MonoBehaviour
{
    [SerializeField] private GameObject[] states;


    public static Automata Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeState(string state)
    {
        foreach(GameObject GO in states)
        {
            GO.SetActive(false);
        }

        foreach(GameObject GO2 in states)
        {
            if(GO2.name == state)
            {
                GO2.SetActive(true);
            }
        }
    }
}
