using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public Text meleeText;
    public Text bulletText;
    public Text speedText;
    public Text fireRate;
    public Text dashForce;

    private void Update()
    {
        meleeText.text = "Melee Damage : " + PlayerManager.Instance.meleeDamage;
        bulletText.text = "Bullet Damage : " + PlayerManager.Instance.bulletDamage;
        speedText.text = "Move Speed : " + PlayerManager.Instance.speed;
        fireRate.text = "Fire Rate : " + PlayerManager.Instance.fireRate;
        dashForce.text = "Dash Force : " + PlayerManager.Instance.dashForce;
    }
}
