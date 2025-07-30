using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;
    public Button[] upgradeButtons;
    public TextMeshProUGUI[] buttonTexts;

    public List<Upgrade> allUpgrades;

    private PlayerXP playerXP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerXP = GameObject.FindWithTag("Player")?.GetComponent<PlayerXP>();
        upgradePanel.SetActive(false);
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

        void ApplyUpgrade(Upgrade upgrade)
        {
            Debug.Log($"Applied upgrade: {upgrade.upgradeName}");
            // TODO: Implement real effects here
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
                Upgrade temp = shuffled[i];
                int rand = Random.Range(i, shuffled.Count);
                shuffled[i] = shuffled[rand];
                shuffled[rand] = temp;
            }

            return shuffled.GetRange(0, Mathf.Min(count, shuffled.Count));
        }
    }
}
