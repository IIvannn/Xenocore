using System.Collections.Generic;
using UnityEngine;

public class BoonSTaticInfo : MonoBehaviour
{
    [Header("Swarm")]
    public static float swarmDamage = 3f;
    public static float swarmAttackSpeed = 0.2f;
    public static float swarmRange = 3f;
    public static float swarmDuration = 5f;
    [Header("Haunted")]
    public static float hauntedInitialDamage = 10f;
    public static float hauntedDamagePercentage = 10f;
    public static float hauntedDuration = 6f;
    public static float hauntedGhostSpeed = 5f;
    [Header("Crystallize")]
    public static float crystallizeCrystalChance = 25f;
    public static int crystallizeCrystalAmmount = 1;
    public static float crystallizeEnergyAmmount = 1.5f;
    public static float crystallizeDuration = 6f;
    [Header("Null")]
    public static float nullRange = 10f;
    public static float nullPullStrength = 2.5f;
    public static float nullDuration = 3f;
    public static int nullMaxCount = 3;
    public static int nullCurrentCount = 0;
    [Header("Starfall")]
    public static float starfallDamage = 53;
    public static float starfallChance = 15;
    public static float starfallDuration = 5;
    [Header("Rust")]
    public static float rustCritChance = 40f;
    public static float rustSelfDamage = 25;
    public static float rustSelfDamageChance = 30;
    public static float rustDuration = 4f;
    [Header("Tectonic")]
    public static float tectonicDamage = 10f;
    public static float tectonicAttackSpeed = 1f;
    public static float tectonicRange = 3f;
    public static float tectonicDuration = 8;
    public static float tectonicMaxCount = 2;
    public static float tectonicCurrentCount = 0;
    public static float tectonicSpreadSpeed = 1.3f;
    [Header("Radiation")]
    public static float radiationWeakness = 20;
    public static float radiationRange = 7f;
    public static float radiationDuration = 8f;


    [Header("Economy")]
    public static int crystals = 0;

    public static  List<Transform> enemiesInRange = new List<Transform>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
