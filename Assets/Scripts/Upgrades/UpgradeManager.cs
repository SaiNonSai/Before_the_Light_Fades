using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject upgradePanel;
    public Button[] upgradeButtons;
    public TextMeshProUGUI[] buttonTexts;

    [Header("Upgrade Options")]
    public List<Upgrade> allUpgrades = new List<Upgrade>();

    private PlayerXP playerXP;

    void Start()
    {
        playerXP = GameObject.FindWithTag("Player")?.GetComponent<PlayerXP>();
        upgradePanel.SetActive(false);

        // Optional: define upgrades in code (or populate via Inspector)
        if (allUpgrades.Count == 0)
        {
            allUpgrades.Add(new Upgrade
            {
                upgradeName = "Speed Boost",
                description = "Move faster!",
                type = UpgradeType.MoveSpeed
            });

            allUpgrades.Add(new Upgrade
            {
                upgradeName = "Stronger Bullets",
                description = "Deal more damage!",
                type = UpgradeType.BulletDamage
            });

            allUpgrades.Add(new Upgrade
            {
                upgradeName = "Multishot",
                description = "Fire 3 bullets at once!",
                type = UpgradeType.Multishot
            });
        }
    }

    public void ShowUpgrades()
    {
        Time.timeScale = 0f;
        upgradePanel.SetActive(true);

        List<Upgrade> randomChoices = GetRandomUpgrades(3);

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i;
            Upgrade upgrade = randomChoices[i];

            buttonTexts[i].text = upgrade.upgradeName;

            upgradeButtons[i].onClick.RemoveAllListeners();
            upgradeButtons[i].onClick.AddListener(() =>
            {
                ApplyUpgrade(upgrade);
                ClosePanel();
            });
        }
    }

    void ApplyUpgrade(Upgrade upgrade)
    {
        GameObject player = GameObject.FindWithTag("Player");

        Debug.Log($"[Upgrade] Applied: {upgrade.upgradeName} — {upgrade.description}");

        switch (upgrade.type)
        {
            case UpgradeType.MoveSpeed:
                player.GetComponent<PlayerMovement>()?.IncreaseMoveSpeed(2f);
                break;

            case UpgradeType.BulletDamage:
                player.GetComponent<PlayerShooting>()?.IncreaseBulletDamage(10);
                break;

            case UpgradeType.Multishot:
                player.GetComponent<PlayerShooting>()?.EnableMultishot();
                break;
        }
    }


    void ClosePanel()
    {
        Time.timeScale = 1f;
        upgradePanel.SetActive(false);
    }

    List<Upgrade> GetRandomUpgrades(int count)
    {
        List<Upgrade> shuffled = new List<Upgrade>(allUpgrades);

        for (int i = 0; i < shuffled.Count; i++)
        {
            int rand = Random.Range(i, shuffled.Count);
            (shuffled[i], shuffled[rand]) = (shuffled[rand], shuffled[i]);
        }

        return shuffled.GetRange(0, Mathf.Min(count, shuffled.Count));
    }
}
