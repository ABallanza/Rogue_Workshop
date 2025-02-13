using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Ability", order = 1)]
public class Upgrade : ScriptableObject
{
    public float rarityPourcentage = 75;

    public string abilityName;
    public int price = 5;
    public string description;

    [Header("Boosts")]
    public float meleeDamageBoost = 0;
    public float bulletDamageBoost = 0;
    public float fireRateBoost = 0;
    public float speedBoost = 0;
    public int lifeBoost = 0;
    public float dashForceBoost = 0;
}