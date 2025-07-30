using UnityEngine;

public enum UpgradeType
{
    MoveSpeed,
    BulletDamage,
    Multishot
}

[System.Serializable]
public class Upgrade
{
    public string upgradeName;
    public string description;
    public Sprite icon; // optional
    public UpgradeType type;
}
