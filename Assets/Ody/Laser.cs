using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Laser : MonoBehaviour
{
    [SerializeField] private float pauseTime;
    [SerializeField] private float laserTime;


    [SerializeField] private GameObject laser;

    public void Start()
    {
        StartCoroutine("StartLaser");
    }

    IEnumerator StartLaser()
    {
        laser.SetActive(false);
        yield return new WaitForSeconds(pauseTime);
        laser.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        laser.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        laser.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        laser.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        laser.SetActive(true);
        yield return new WaitForSeconds(laserTime);
        laser.SetActive(false);
        StartCoroutine("StartLaser");
    }
}
