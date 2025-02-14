using UnityEngine;

public class Baka : MonoBehaviour
{
    
    public void PlayCombat()
    {
        Application.LoadLevel("FightTest");
    }

    public void PlayBoss()
    {
        Application.LoadLevel("FightBoss");
    }


}
