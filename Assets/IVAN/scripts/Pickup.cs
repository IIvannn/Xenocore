using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public string reward;
    public float amount;
    public float interactionrange;
    public GameObject closeIndicator;
    public TextMeshProUGUI pricetag;
    public bool priced = false;
    public int price = 1;
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
        if (PlayerDamage.dead)
        {
            return;
        }


        float distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);
        if (distance < interactionrange)
        {
            closeIndicator.SetActive(true);
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Pick();
                
                
            }
        }
        else
        {
            closeIndicator.SetActive(false);
        }
    }

    void Pick()
    {

        if (priced && BoonSTaticInfo.crystals >= price)
        {
            switch (reward)
            {
                case "crystal":
                    BoonSTaticInfo.crystals += (int)(amount * BoonSTaticInfo.moneyMultiplier);
                    Destroy(gameObject);
                    return;
                case "health":
                    PlayerDamage.currentHp += (int)(amount * BoonSTaticInfo.healingMultiplier);
                    Destroy(gameObject);
                    return;
                case "maxHealth":
                    PlayerDamage.currentHp += (int)(amount * BoonSTaticInfo.healingMultiplier);
                    PlayerDamage.hp += (int)(amount);
                    Destroy(gameObject);
                    return;
                
            }
        }
        else if (!priced)
        {
            UpgradeManager.upgradeTaken = true;
            switch (reward)
            {
                case "crystal":
                    BoonSTaticInfo.crystals += (int)(amount * BoonSTaticInfo.moneyMultiplier);
                    Destroy(gameObject);
                    return;
                case "health":
                    PlayerDamage.currentHp += (int)(amount * BoonSTaticInfo.healingMultiplier);
                    Destroy(gameObject);
                    return;
                case "maxHealth":
                    PlayerDamage.currentHp += (int)(amount * BoonSTaticInfo.healingMultiplier);
                    PlayerDamage.hp += (int)(amount);
                    Destroy(gameObject);
                    return;
                case "essence":
                    BoonSTaticInfo.essence++;
                    Destroy(gameObject);
                    return;

            }
        }

        
    }
}
