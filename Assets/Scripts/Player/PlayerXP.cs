using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerXP : MonoBehaviour
{
    public int currentXP = 0;
    public int level = 1;
    public int xpToNextLevel = 100;

    public Text xpText;

    public UpgradeManager upgradeManager;

    void Start()
    {
        UpdateXPUI();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateXPUI();
    }

    void LevelUp()
    {
        level++;
        currentXP -= xpToNextLevel;
        xpToNextLevel += 50;
        Debug.Log("Level Up! Now level " + level);

        if (upgradeManager != null)
        {
            upgradeManager.ShowUpgrades();
        }

    }

    void UpdateXPUI()
    {
        if (xpText != null)
        {
            xpText.text = $"XP: {currentXP} / {xpToNextLevel}";
        }
    }
}
