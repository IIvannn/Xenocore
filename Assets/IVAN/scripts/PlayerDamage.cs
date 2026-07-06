
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    [Header("References")]
    public Slider healthbar;
    public Slider easeHealthbar;
    public Slider energybar;
    public Slider armorbar;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI crystalText;
    public GameObject upgrader;
    public GameObject shockwave;
    public GameObject inv;
    float lerpSpeed = 0.03f;

    [Header("Health")]
    public bool invincible = false;
    public static float hp = 100f;
    public static float currentHp = 100f;
    public float maxHp = 100;
    public static bool dead = false;
    public static float playerArmor = 0;
    public float dr = 1;
    public static bool tr = false;
    [Header("Energy")]
    public static float energy = 100;
    public static float currentEnergy = 0;


    float crystalerp;
    float hplerp;
    float energylerp;

    Scene m_Scene;
    string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        Debug.Log(sceneName);
        if (sceneName == "Spawnroom")
        {
            hp = maxHp;
        }
        
        //currentHp = hp;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            inv.SetActive(true);
        }
        else
        {
            inv.SetActive(false);
        }

        energy = BoonSTaticInfo.spellCost;


        healthbar.value = (currentHp/hp);
        armorbar.value = playerArmor/hp;
        easeHealthbar.value = Mathf.Lerp(easeHealthbar.value, healthbar.value, lerpSpeed);
        energybar.value = (currentEnergy / energy);

        hplerp = Mathf.Lerp(hplerp, currentHp+playerArmor, lerpSpeed);
        energylerp = Mathf.Lerp(energylerp, currentEnergy, lerpSpeed);
        crystalerp = Mathf.Lerp(crystalerp, BoonSTaticInfo.crystals, lerpSpeed);

        crystalText.text = ((int)crystalerp+1).ToString();
        healthText.text = (((int)hplerp + 1) + "/" + hp);
        energyText.text = (((int)energylerp + 1) + "/" + energy);

        if (currentHp > hp)
        {
            currentHp = hp;
        }
        if (currentEnergy > energy)
        {
            currentEnergy = energy;
        }
    }

    public void TakeDamage(float damage)
    {
        if (invincible)
        {
            return;
        }

        if (tr)
        {
            dr = 0.7f;
            damage *= dr;
        }
        


        if (BoonSTaticInfo.noYou)
        {
            float nychance = Random.Range(1, 100);
            if (nychance < BoonSTaticInfo.noYouChance)
            {
                GameObject ball = Instantiate(shockwave, transform.position, transform.rotation);
                ball.GetComponent<Shockwave>().damage = damage;
                ball.GetComponent<Shockwave>().range = BoonSTaticInfo.noYouRange;
                ball.GetComponent<Shockwave>().type = "star";
                return;
            }
        }
        
        if (BoonSTaticInfo.untouchable)
        {
            float rchance = Random.Range(1, 100);
            if (rchance >=BoonSTaticInfo.untouchableChance)
            {
                currentHp -= damage;
                if (currentHp <= 0)
                {
                    Death();
                }
            }
            else
            {
                Debug.Log("Dodge!");
            }
        }
        else if (BoonSTaticInfo.crystallineArmor)
        {
            float rchance = Random.Range(1, 100);
            if (rchance < BoonSTaticInfo.crystallineArmorChance)
            {
                BoonSTaticInfo.crystals -= (int)damage;
            }
            else
            {
                currentHp -= damage;
                if (currentHp <= 0)
                {
                    Death();
                }
            }
        }
        else
        {
            if (playerArmor >0)
            {
                playerArmor -= damage;
                if (playerArmor<0)
                {
                    playerArmor = 0;
                }
            }
            else
            {
                currentHp -= damage;
                if (currentHp <= 0)
                {
                    Death();
                }
            }
                
        }
    }

    public void Death()
    {
        dead = true;
        BoonSTaticInfo.RESETUPGRADES();
        SceneManager.LoadScene("DeathMenu");
        dead = false;
        //Destroy(gameObject);
    }

}
