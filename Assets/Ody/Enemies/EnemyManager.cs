using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyAutomata automata;


    [SerializeField] private float life = 100;

    [Range(0, 10)] public int shardsToGive;


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
            PlayerManager.Instance.shards += shardsToGive;
            Destroy(gameObject);
        }
    }
}
