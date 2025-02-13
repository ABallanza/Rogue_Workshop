using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public Text meleeText;
    public Text bulletText;
    public Text speedText;


    private void Update()
    {
        meleeText.text = "Melee Damage : " + PlayerManager.Instance.meleeDamage;
        bulletText.text = "Bullet Damage : " + PlayerManager.Instance.bulletDamage;
        speedText.text = "Move Speed : " + PlayerManager.Instance.speed;
    }
}
