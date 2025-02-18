using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Laser : MonoBehaviour
{
    [SerializeField] private float pauseTime;
    [SerializeField] private float laserTime;


    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject laserIndicator;

    private float laserLength;

    public void Start()
    {
        laserLength = laser.transform.localScale.y;
        laserIndicator.transform.localScale = new Vector3(laserIndicator.transform.localScale.x, laserLength, laserIndicator.transform.localScale.z);
        StartCoroutine("StartLaser");
    }

    IEnumerator StartLaser()
    {
        laser.SetActive(false);
        yield return new WaitForSeconds(pauseTime);


        
        laserIndicator.SetActive(true);
        yield return new WaitForSeconds(.2f);
        laserIndicator.SetActive(false);
        yield return new WaitForSeconds(.2f);

        laserIndicator.SetActive(true);
        yield return new WaitForSeconds(.2f);
        laserIndicator.SetActive(false);
        yield return new WaitForSeconds(.2f);

        laserIndicator.SetActive(true);
        yield return new WaitForSeconds(.2f);
        laserIndicator.SetActive(false);
        yield return new WaitForSeconds(.2f); 


        laser.SetActive(true);
        yield return new WaitForSeconds(laserTime);

        laser.SetActive(false);
        StartCoroutine("StartLaser");
    }
}
