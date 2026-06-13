using UnityEngine;


[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades/New upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea]public string description;
    public Sprite upgradeIcon;
}
