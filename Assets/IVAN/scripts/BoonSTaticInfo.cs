using System.Collections.Generic;
using UnityEngine;

public class BoonSTaticInfo : MonoBehaviour
{
    [Header("Swarm")]
    public static float swarmDamage = 4f;
    public static float swarmAttackSpeed = 0.4f;
    public static float swarmRange = 3f;
    public static float swarmDuration = 5f;
    [Header("Haunted")]
    public static float hauntedInitialDamage = 30f;
    public static float hauntedDamagePercentage = 40f;
    public static float hauntedDuration = 3f;
    public static float hauntedGhostSpeed = 5f;
    [Header("Crystallize")]
    public static float crystallizeCrystalChance = 25f;
    public static int crystallizeCrystalAmmount = 1;
    public static float crystallizeEnergyAmmount = 1.5f;
    public static float crystallizeDuration = 6f;
    [Header("Null")]
    public static float nullRange = 10f;
    public static float nullPullStrength = 1.8f;
    public static float nullDuration = 3f;
    public static int nullMaxCount = 3;
    public static int nullCurrentCount = 0;
    [Header("Starfall")]
    public static float starfallDamage = 66;
    public static float starfallChance = 17;
    public static float starfallDuration = 5;
    [Header("Rust")]
    public static float rustCritChance = 35f;
    public static float rustSelfDamage = 25;
    public static float rustSelfDamageChance = 40;
    public static float rustDuration = 4f;
    [Header("Tectonic")]
    public static float tectonicDamage = 50f;
    public static float tectonicAttackSpeed = 2.6f;
    public static float tectonicRange = 2f;
    public static float tectonicDuration = 7.8f;
    public static float tectonicMaxCount = 2;
    public static float tectonicCurrentCount = 0;
    public static float tectonicSpread = 1.5f;
    public static float tectonicSpreadSpeed = 0.6f;
    [Header("Radiation")]
    public static float radiationWeakness = 22;
    public static float radiationRange = 7f;
    public static float radiationDuration = 8f;
    public static int radiationMaxCount = 4;
    public static int radiationCurrentCount = 0;


    [Header("Economy")]
    public static int crystals = 0;

    [Header("General info")]
    public static float healingMultiplier = 1f;
    public static float moneyMultiplier = 1f;
    public static float doorHeal = 5f;
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

    

    [Header("Swarm Boons")]
    public static bool silky = false;
    public static bool adaptation = false;
    public static bool corrosive = false;
    public static float corrosiveBonus = 1.5f;
    public static bool infestation = false;
    public static bool nested = false;
    public static bool pob = false;
    public static float waspDamage = 10f;
    public static float waspSpeed = 10f;
    public static float proliferationBonus = 1.5f;
    public static float frenzyBonus = 1.5f;


    [Header("Haunted Boons")]
    public static float dreadBonus = 15f;
    public static bool emotionalDamage = false;
    public static float emotionalDamageBonus = 15f;
    public static bool exorcism = false;
    public static float exorcismBonus = 30f;
    public static float jumpscareBonus = 35f;
    public static bool necromancer = false;
    public static float phantomDamage = 10f;
    public static float phantomSpeed = 10f;
    public static float phantomAttackSpeed = 0.8f;
    public static bool phaseDash = false;
    public static float phaseDashDashReduction = 1.5f;
    public static float phaseDashDashDamage = 20f;
    public static bool posession = false;
    public static bool reaper = false;
    public static float reaperBonus = 20f;
    public static bool untouchable = false;
    public static float untouchableChance = 10f;


    [Header("Crystallize Boons")]
    public static bool arcaneSwiftness = false;
    public static float arcaneSwiftnessBonus = 1.20f;
    public static bool crystallineArmor = false;
    public static float crystallineArmorChance = 15f;
    public static bool energized = false;
    public static float energizedBonus = 8f;
    public static float excavationBonus = 0.20f;
    public static float fortuneBonus = 12f;
    public static bool medusa = false;
    public static float petrifyDuration = 1.3f;
    public static bool monopoly = false;
    public static float momopolyBonus = 0.02f;
    public static bool overgrowth = false;
    public static float prismDuration = 10f;
    public static float prismDamage = 25f;
    public static float prismRange = 8f;
    public static float prismCooldown = 0.8f;
    public static bool resonance = false;
    public static float resonanceDamage = 22f;

    [Header("Null Boons")]
    public static bool farFar = false;
    public static float farFarBonus = 0.1f;
    public static bool collapse = false;
    public static float collapseDamage = 50f;
    public static bool exponentiallity = false;
    public static float exponentiallityBonus = 0.2f;
    public static float graspBonus = 3.5f;
    public static float gravityBonus = 0.5f;
    public static bool massAccumulation = false;
    public static float massAccumulationBonus = 3f;
    public static bool multiversalStrike = false;
    public static float multiversalStrikeDamage = 10;
    public static float multiversalStrikeCooldown = 2f;
    public static bool noEscape = false;
    public static float noEscapeSpeed = 3;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
