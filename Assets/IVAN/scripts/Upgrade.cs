using UnityEngine;
using UnityEngine.InputSystem;

public class Upgrade : MonoBehaviour
{
    public string type;
    bool close = false;
    public GameObject closeIndicator;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (close && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Pick();
        }

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

        switch (type)
        {
            case "boomerang":
                upgradeManager.Upgrader("boomerang");
                return;

            case "swarm":
                upgradeManager.Upgrader("swarm");
                return;
            case "haunted":
                upgradeManager.Upgrader("haunted");
                return;
            case "crystallize":
                upgradeManager.Upgrader("crystallize");
                return;
            case "null":
                upgradeManager.Upgrader("null");
                return;
            case "starfall":
                upgradeManager.Upgrader("starfall");
                return;
            case "rust":
                upgradeManager.Upgrader("rust");
                return;
            case "tectonic":
                upgradeManager.Upgrader("tectonic");
                return;
            case "radiation":
                upgradeManager.Upgrader("radiation");
                return;

        }
    }
}

