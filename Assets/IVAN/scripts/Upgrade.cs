using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public string type;
    bool close = false;
    public GameObject closeIndicator;
    public TextMeshProUGUI pricetag;
    GameObject player;

    public bool priced = false;
    public int price = 1;
    bool opened = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pricetag.gameObject.SetActive(false);
        closeIndicator.SetActive(false);

        if (priced)
        {
            pricetag.gameObject.SetActive(true);
            pricetag.text = price.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (close && Keyboard.current.eKey.wasPressedThisFrame)
        {

            if (priced && BoonSTaticInfo.crystals >= price)
            {
                BoonSTaticInfo.crystals -= price;
                Pick();
                
            }
            else if (!priced)
            {
                Pick();
                
            }
            
        }
        //if (UpgradeManager.destroyUpgrades)
        //{
        //    UpgradeManager.destroyUpgrades = false;
        //    Destroy(gameObject);
        //}


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            close = true;
            closeIndicator.SetActive(true);
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            close = false;
            closeIndicator.SetActive(false);
            player = null;
        }
    }


    void Pick()
    {
        

        GameObject upgrader = player.GetComponent<PlayerDamage>().upgrader;
        upgrader.SetActive(true);
        UpgradeManager upgradeManager = upgrader.GetComponent<UpgradeManager>();
        //upgradeManager.ChoseUpgrades();
        switch (type)
        {
            case "boomerang":
                upgradeManager.Upgrader("boomerang",opened);
                opened = true;
                Destroy(gameObject);
                return;
            case "swarm":
                upgradeManager.Upgrader("swarm", opened);
                opened = true;
                Destroy(gameObject);
                return;
            case "haunted":
                upgradeManager.Upgrader("haunted", opened);
                opened = true;
                Destroy(gameObject);
                return;
            case "crystallize":
                upgradeManager.Upgrader("crystallize", opened);
                opened = true;
                Destroy(gameObject);
                return;
            case "null":
                upgradeManager.Upgrader("null", opened);
                opened = true;
                Destroy(gameObject);
                return;
            case "starfall":
                upgradeManager.Upgrader("starfall", opened);
                opened = true;
                Destroy(gameObject);
                return;
            case "rust":
                upgradeManager.Upgrader("rust", opened);
                opened = true;
                Destroy(gameObject);
                return;
            case "tectonic":
                upgradeManager.Upgrader("tectonic", opened);
                opened = true;
                Destroy(gameObject);
                return;
            case "radiation":
                upgradeManager.Upgrader("radiation", opened);
                opened = true;
                Destroy(gameObject);
                return;
        }
    }
}

