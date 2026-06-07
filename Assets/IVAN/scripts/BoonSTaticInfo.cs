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
    public static float nullRange;
    public static float nullSlow;
    public static float nullPullStrength;
    public static float nullDuration;
    [Header("Starfall")]
    public static float starfallDamage = 50;
    public static float starfallChance = 20;
    public static float starfallDuration = 5;
    [Header("Rust")]
    public static float rustCritChance;
    public static float rustSelfDamage;
    public static float rustDuration;
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
