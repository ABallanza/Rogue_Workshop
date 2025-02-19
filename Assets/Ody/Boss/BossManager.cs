using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class BossManager : MonoBehaviour
{
    public Slider bossLife;

    public float life = 500f;
    float maxLife;

    [Header("Laser")]
    public Transform laserPoint;

    public Transform spawnPoint1;

    public LineRenderer lr;

    public Transform bossRoot;


    private void Start()
    {
        maxLife = life;
        DesactivateLaser();
    }


    bool isDead = false;

    


    public void ActivateLaser()
    {
        lr.enabled = true;
        laserPoint.gameObject.SetActive(true);
    }

    public void DesactivateLaser()
    {
        lr.enabled = false;
        laserPoint.gameObject.SetActive(false);
    }


    public void FixedUpdate()
    {
        Transform player = GameObject.Find("Player").transform;
        bossRoot.position = Vector3.MoveTowards(bossRoot.position, new Vector3(player.position.x, bossRoot.position.y, bossRoot.position.z), 0.04f);
        laserPoint.position = Vector3.MoveTowards(laserPoint.position, new Vector3(player.position.x, laserPoint.position.y, player.position.z), 0.1f);
    }



    public void Update()
    {
        lr.SetPosition(0, spawnPoint1.position);
        lr.SetPosition(1, laserPoint.position);

        bossLife.value = life / maxLife;

        if(life <= 0 && !isDead)
        {
            isDead = true;
            Application.LoadLevel("Hub");
        }
    }
}
