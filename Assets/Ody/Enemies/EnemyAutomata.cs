using UnityEngine;

public class EnemyAutomata : MonoBehaviour
{
    public GameObject[] states;



    public void ChangeState(string state)
    {
        foreach(GameObject go in states)
        {
            go.SetActive(false);
        }

        foreach(GameObject go in states)
        {
            if(go.name == state)
            {
                go.SetActive(true);
            }
        }
    }
}
