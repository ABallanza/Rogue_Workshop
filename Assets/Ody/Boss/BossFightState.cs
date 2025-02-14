using System.Collections;
using UnityEngine;

public class BossFightState : MonoBehaviour
{
    public EnemyAutomata automata;

    public string animName;

    public Animator anims;
    public float timeBeforeIdle;


    public void OnEnable()
    {
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim()
    {
        anims.Play(animName);
        yield return new WaitForSeconds(timeBeforeIdle);
        anims.SetTrigger("EndAttack");
        automata.ChangeState("BaseState");
    }
}
