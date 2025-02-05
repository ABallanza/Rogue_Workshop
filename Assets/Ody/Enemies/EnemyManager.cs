using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyAutomata automata;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            automata.ChangeState("DamageState");
        }
    }
}
