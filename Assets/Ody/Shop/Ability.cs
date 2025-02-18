using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [Header("Information")]
    public Text _name;
    public Text price;
    public Text description;
    public Text rarity;
    public int realPrice;

    [Header("Boost")]
    [HideInInspector] public float meleeDamageBoost = 0;
    [HideInInspector] public float bulletDamageBoost = 0;
    [HideInInspector] public float fireRateBoost = 0;
    [HideInInspector] public float speedBoost = 0;
    [HideInInspector] public int lifeBoost = 0;
    [HideInInspector] public float dashForceBoost = 0;


    public void Use()
    {
        if(PlayerManager.Instance.shards >= realPrice)
        {
            PlayerManager.Instance.shards -= realPrice;
            PlayerManager.Instance.meleeDamage += meleeDamageBoost;
            PlayerManager.Instance.bulletDamage += bulletDamageBoost;
            PlayerManager.Instance.fireRate -= fireRateBoost;
            PlayerManager.Instance.speed += speedBoost;
            PlayerManager.Instance.AddLife(lifeBoost);
            PlayerManager.Instance.dashForce += dashForceBoost;

            GetComponentInParent<Chest>().CloseShop();
        }
        else
        {
            GetComponentInParent<Chest>().CloseShop();
        }
    }
}
