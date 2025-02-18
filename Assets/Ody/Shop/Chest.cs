using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour
{
    public GameObject text;

    public PlayerInput playerInput;

    public bool canOpen = false;

    public bool openned = false;

    public GameObject shopCanvas;

    public Upgrade[] upgrades;

    [Header("Upgrades")]
    public Upgrade upgrade_1;
    public Upgrade upgrade_2;
    public Upgrade upgrade_3;


    public Ability[] abilities;

    bool used = false;


    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Interagir.performed += ctx => OpenChest();
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.Interagir.performed -= ctx => OpenChest();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !openned)
        {
            text.SetActive(true);
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            text.SetActive(false);
            canOpen = false;
        }
    }

    public void OpenChest()
    {

        if (!used)
        {
            used = true;
            GetUpgrades();

            if (!openned && canOpen)
            {
                openned = true;
                text.SetActive(false);
                shopCanvas.SetActive(true);
            }
        }
    }

    void GetUpgrades()
    {
        List<Upgrade> possibleUpgrades = new List<Upgrade>();

        foreach (Upgrade upgrade in upgrades)
        {
            int chances = Mathf.RoundToInt(upgrade.rarityPourcentage);
            for (int i = 0; i < chances; i++)
            {
                possibleUpgrades.Add(upgrade);
            }
        }

        if (possibleUpgrades.Count < 3)
        {
            return;
        }

        // Shuffle list
        for (int i = 0; i < possibleUpgrades.Count; i++)
        {
            Upgrade temp = possibleUpgrades[i];
            int randomIndex = Random.Range(i, possibleUpgrades.Count);
            possibleUpgrades[i] = possibleUpgrades[randomIndex];
            possibleUpgrades[randomIndex] = temp;
        }

        // Select unique upgrades
        HashSet<Upgrade> selectedUpgrades = new HashSet<Upgrade>();
        while (selectedUpgrades.Count < 3)
        {
            Upgrade randomUpgrade = possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
            selectedUpgrades.Add(randomUpgrade);
        }

        Upgrade[] chosenUpgrades = new Upgrade[3];
        selectedUpgrades.CopyTo(chosenUpgrades);

        upgrade_1 = chosenUpgrades[0];
        upgrade_2 = chosenUpgrades[1];
        upgrade_3 = chosenUpgrades[2];

        // Assign upgrades to abilities
        AssignUpgradeToAbility(abilities[0], upgrade_1);
        AssignUpgradeToAbility(abilities[1], upgrade_2);
        AssignUpgradeToAbility(abilities[2], upgrade_3);
    }

    void AssignUpgradeToAbility(Ability ability, Upgrade upgrade)
    {
        ability._name.text = upgrade.abilityName;
        ability.price.text = "Price : " + upgrade.price.ToString();
        ability.realPrice = upgrade.price;
        ability.description.text = upgrade.description;
        ability.rarity.text = "Chance : " + upgrade.rarityPourcentage.ToString();

        // Apply all boosts
        ability.meleeDamageBoost = upgrade.meleeDamageBoost;
        ability.bulletDamageBoost = upgrade.bulletDamageBoost;
        ability.fireRateBoost = upgrade.fireRateBoost;
        ability.speedBoost = upgrade.speedBoost;
        ability.lifeBoost = upgrade.lifeBoost;
        ability.dashForceBoost = upgrade.dashForceBoost;
    }


    public void CloseShop()
    {
        text.SetActive(false);
        shopCanvas.SetActive(false);
    }

}
