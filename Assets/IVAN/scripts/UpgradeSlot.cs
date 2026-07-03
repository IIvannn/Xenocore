using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    public int slot;
    public UpgradeData ud;
    public string upgrade = "no upgrade";
    public Image upgradeIcon;
    public TextMeshProUGUI upgradeText;
    public GameObject upgradeManager;

    void Start()
    {

    }

    public void AttributesSet()
    {
        if (ud != null)
        {
            upgrade = ud.upgradeName;
            upgradeText.text = ud.description;
            upgradeIcon.sprite = ud.upgradeIcon;

        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    public void UpgradeSelected()
    {
        //Debug.Log(upgrade+"  chosen");
        upgradeManager.GetComponent<UpgradeManager>().UpgradeSelected(upgrade, slot);
    }

}
