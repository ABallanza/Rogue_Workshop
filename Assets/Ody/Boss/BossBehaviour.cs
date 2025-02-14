using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{

    public Transform boss;

    public GameObject[] firstStates;

    public EnemyAutomata automata;

    private void OnEnable()
    {
        StartCoroutine(RandomState());
    }


    IEnumerator RandomState()
    {
        yield return new WaitForSeconds(5f);
        automata.ChangeState(firstStates[Random.Range(0, firstStates.Length)].name);
        print(firstStates[Random.Range(0, firstStates.Length)].name);
    }


    public void Update()
    {
        Transform player = GameObject.Find("Player").transform;
        boss.position = Vector3.Lerp(boss.position, new Vector3(player.position.x, boss.position.y, boss.position.z), 0.005f);
    }
}
