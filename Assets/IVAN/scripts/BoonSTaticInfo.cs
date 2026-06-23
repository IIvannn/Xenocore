using System.Collections.Generic;
using UnityEngine;

public class BoonSTaticInfo : MonoBehaviour
{
    [Header("Swarm")]
    public static float swarmDamage = 6f;
    public static float swarmAttackSpeed = 0.4f;
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
    public static float nullPullStrength = 2.2f;
    public static float nullDuration = 3f;
    public static int nullMaxCount = 3;
    public static int nullCurrentCount = 0;
    [Header("Starfall")]
    public static float starfallDamage = 45;
    public static float starfallChance = 10;
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
    public static float radiationWeakness = 25;
    public static float radiationRange = 7f;
    public static float radiationDuration = 8f;
    public static int radiationMaxCount = 4;
    public static int radiationCurrentCount = 0;


    [Header("Economy")]
    public static int crystals = 0;

    public static  List<Transform> enemiesInRange = new List<Transform>();



    public static bool hasAttack = false;
    public static bool hasSpecial = false;
    public static bool hasSpell = false;

    public static bool hasSwarm = false;
    public static bool hasHaunted = false;
    public static bool hasCrystallize = false;
    public static bool hasNull = false;
    public static bool hasStarfall = false;
    public static bool hasRust = false;
    public static bool hasTectonic = false;
    public static bool hasRadiation = false;

    [Header("Boomerang Boons")]
    public static bool boomerangGrow = false;
    public static float boomerangGrowBonus = 0.2f;
    public static bool boomerangMark = false;
    public static bool boomerangReturnShockwave = false;
    public static bool boomerangSpeed = false;
    public static float boomerangSpeedBonus = 15f;
    public static float boomerangSpecialSpeedBonus = 250f;
    public static bool boomerangSpecialGrow = false;
    public static float boomerangSpecialGrowBonus = 8f;
    public static bool boomerangSpecialCooldwon = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
