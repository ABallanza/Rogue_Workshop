using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossDamageZone : MonoBehaviour
{

    public BossManager bossManager;


    

    public void TakeDamage(float damage)
    {
        bossManager.life -= damage;
    }



}
