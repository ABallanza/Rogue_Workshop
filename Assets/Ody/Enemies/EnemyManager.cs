using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyAutomata automata;


    [SerializeField] private float life = 100;

    [Range(0, 10)] public int shardsToGive;

    [Range(0, 100)] public int chanceToDropGem;

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
            PlayerManager.Instance.AddShard("SHARD", shardsToGive.ToString(), shardsToGive);

            int i = Random.Range(0, 100);
            if(i <= chanceToDropGem)
            {
                PlayerManager.Instance.AddGem("Gem", "1");
            }

            Gun.Instance.Kill();
            Destroy(gameObject);
        }
    }
}
