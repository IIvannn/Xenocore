using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    public UpgradeData ud;
    public string upgrade = "no upgrade";
    public Image upgradeIcon;
    public TextMeshProUGUI upgradeText;

    void Start()
    {
        if (ud != null)
        {
            upgrade = ud.upgradeName;
            upgradeText.text = ud.description;
            upgradeIcon.sprite = ud.upgradeIcon;
            
        }
    }

    void UpgradeSelected()
    {
        Debug.Log(upgrade);
    }
}
