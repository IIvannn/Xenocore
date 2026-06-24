using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public string reward;
    public float amount;
    public float interactionrange;
    public GameObject closeIndicator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                Destroy(gameObject);
                
            }
        }
        else
        {
            closeIndicator.SetActive(false);
        }
    }

    void Pick()
    {
        switch (reward)
        {
            case "crystal":
                BoonSTaticInfo.crystals += (int)(amount* BoonSTaticInfo.moneyMultiplier);
                return;
            case "health":
                PlayerDamage.currentHp += (int)(amount);
                return;
            case "maxHealth":
                PlayerDamage.currentHp += (int)(amount*BoonSTaticInfo.healingMultiplier);
                PlayerDamage.hp += (int)(amount);
                return;

        }
    }
}
