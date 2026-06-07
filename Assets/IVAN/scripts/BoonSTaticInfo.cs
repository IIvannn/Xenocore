using UnityEngine;

public class BoonSTaticInfo : MonoBehaviour
{
    [Header("Swarm")]
    public static float swarmDamage = 2f;
    public static float swarmAttackSpeed = 0.3f;
    public static float swarmRange = 3f;
    public static float swarmDuration = 5f;
    [Header("Haunted")]
    public static float hauntedBaseDamage;
    public static float hauntedDamagePercentage;
    public static float hauntedDuration;
    [Header("Crystallize")]
    public static float crystallizeCrystalChance;
    public static float crystallizeEnergyChance;
    public static float crystallizeDuration;
    [Header("Null")]
    public static float nullRange = 10f;
    public static float nullPullStrength = 2.5f;
    public static float nullDuration = 3f;
    public static int nullMaxCount = 3;
    public static int nullCurrentCount = 0;
    [Header("Starfall")]
    public static float starfallDamage = 60;
    public static float starfallChance = 20;
    public static float starfallDuration = 5;
    [Header("Rust")]
    public static float rustCritChance = 40f;
    public static float rustSelfDamage;
    public static float rustDuration = 4f;
    [Header("Tectonic")]
    public static float tectonicDamage;
    public static float tectonicAttackSpeed;
    public static float tectonicDuration;
    public static float tectonicCooldown;
    [Header("Radiation")]
    public static float radiationWeakness;
    public static float radiationRange;
    public static float radiationCloseStrength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
