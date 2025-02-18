using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public Slider bossLife;

    public float life = 500f;
    float maxLife;


    private void Start()
    {
        maxLife = life;
    }


    public void Update()
    {
        bossLife.value = life / maxLife;
    }
}
