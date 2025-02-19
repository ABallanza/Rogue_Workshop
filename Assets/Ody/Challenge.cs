using System.Collections;
using UnityEngine;

public class Challenge : MonoBehaviour
{

    public GameObject[] spawnPoints;

    int waveNumber;

    public GameObject[] wave_1;
    public GameObject[] wave_2;
    public GameObject[] wave_3;

    public int totalWaves = 1;

    public GameObject endDoor;
    public GameObject[] redThings;

    public GameObject[] enemies;

    public void Start()
    {
        StartWave();
        GameObject.Find("Player").GetComponent<Rigidbody>().position = transform.position;
    }

    void StartWave()
    {
        waveNumber++;
        int v = 0;

        if(waveNumber == 1)
        {
            foreach (GameObject GO in wave_1)
            {
                Instantiate(GO, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
                v++;
            }
        }
        if(waveNumber == 2)
        {
            foreach (GameObject GO in wave_2)
            {
                Instantiate(GO, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
                v++;
            }
        }
        if(waveNumber == 3)
        {
            foreach (GameObject GO in wave_3)
            {
                Instantiate(GO, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
                v++;
            }
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        StartCoroutine("CheckEndWave");
    }

    public IEnumerator CheckEndWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            // Dynamically update the enemies list
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length == 0)
            {
                if (waveNumber < totalWaves)
                {
                    StartWave();
                }
                else
                {
                    endDoor.SetActive(true);
                    redThings[0].SetActive(false);
                    redThings[1].SetActive(false);
                }
                yield break; // Exit the coroutine once the wave is over
            }
        }
    }



}
