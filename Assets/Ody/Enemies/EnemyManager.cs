using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyAutomata automata;


    [SerializeField] private float life = 100;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            automata.ChangeState("DamageState");
        }
    }


    public void TakeDamage(float damage)
    {
        life -= damage;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
