using System.Collections;
using System.Transactions;
using UnityEngine;
using static UnityEngine.Android.AndroidGame;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private float pushForce;

    public bool doFreeze = false;

    public bool isProjectile = false;

    public float damage = 15f;

    private void OnEnable()
    {
        if(!isProjectile)
        {
            damage = PlayerManager.Instance.meleeDamage;
        }
        else
        {
            damage = PlayerManager.Instance.bulletDamage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<EnemyManager>())
            {
                other.GetComponent<EnemyManager>().TakeDamage(damage);
                other.GetComponent<EnemyManager>().automata.ChangeState("DamageState");
                other.GetComponent<Rigidbody>().linearVelocity = PlayerManager.Instance.model.right * pushForce/1.2f;
                CameraRoot.Instance.ShakeCam();
                if (doFreeze)
                {
                    StartCoroutine(Freeze());
                }
            }
        }
        if(other.tag == "Enemy")
        {
            if (other.GetComponent<BossDamageZone>())
            {
                other.GetComponent<BossDamageZone>().TakeDamage(damage);
                CameraRoot.Instance.ShakeCam();
            }
        }
    }

    IEnumerator Freeze()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1;
    }
}
