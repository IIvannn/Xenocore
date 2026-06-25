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
    public static List<GameObject> enemiesAlive = new List<GameObject>();



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
    public static float silkyBonus = 0.3f;
    public static bool adaptation = false;
    public static float adaptationLimit = 0.3f;
    public static bool corrosive = false;
    public static float corrosiveBonus = 0.5f;
    public static bool infestation = false;
    public static float infestationChance = 35f;
    public static bool nested = false;
    public static float nestedChance = 15f;
    public static bool pob = false;
    public static float nestDuration = 8f;
    public static float waspSpawnSpeed = 1f;
    public static float waspDamage = 10f;
    public static float waspSpeed = 10f;
    public static float proliferationBonus = 1.5f;
    public static float frenzyBonus = 0.15f;


    [Header("Haunted Boons")]
    public static float dreadBonus = 15f;
    public static bool emotionalDamage = false;
    public static float emotionalDamageBonus = 0.2f;
    public static bool exorcism = false;
    public static float exorcismBonus = 10f;
    public static float jumpscareBonus = 35f;
    public static bool necromancer = false;
    public static float necroancerChance = 15f;
    public static bool phaseDash = false;
    public static float phaseDashDashDamage = 20f;
    public static float phaseDashDashDurationBonus = 0.2f;
    public static bool posession = false;
    public static float possessionChance = 5f;
    public static bool reaper = false;
    public static int reaperBonus = 0;
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


    [Header("Starfall Boons")]
    public static float clearSkyBonus = 10f;
    public static bool crater = false;
    public static float craterRange = 3f;
    public static bool doubleTrouble = false;
    public static bool fated = false;
    public static float fatedBonus = 1.25f;
    public static bool luckyStar = false;
    public static bool makeAWish = false;
    public static float makeAWishCC = 25;
    public static float makeAWishCD = 1.3f;
    public static float meteoriteBonus = 15f;
    public static bool noYou = false;
    public static float noYouChance = 10f;
    public static bool shenanigans = false;


    [Header("Rust Boons")]
    public static bool cracked = false;
    public static float crackedBonus = 30f;
    public static bool embrittled = false;
    public static bool fatigue = false;
    public static float fatigueBonus = 3f;
    public static bool galvanized = false;
    public static float galvanizedBonus = 1.2f;
    public static float mutilationBonus = 20f;
    public static float oxidisedBonus = 20f;
    public static bool purge = false;
    public static bool soldering = false;
    public static float solderingBonus = 3f;
    public static bool tetanus = false;
    public static float tetanusChance = 10f;



    [Header("Tectonic Boons")]
    public static bool faceOff = false;
    public static float faceOffBonus = 25f;
    public static bool molten = false;
    public static float moltenBonus = 0.3f;
    public static bool mudbath = false;
    public static float mudbathChance = 25f;
    public static float mudbathMax = 15f;
    public static bool eruption = false;
    public static float eruptionDuration = 2f;
    public static float eruptionDamage = 20f;
    public static float shatterBonus = 18f;
    public static bool tremble = false;
    public static int staggerCap = 4;
    public static float staggerDuration = 1.5f;
    public static float tremorBonus = 1f;
    public static bool troglodite = false;
    public static bool volcanic = false;
    public static float volcanicChance = 35f;
    public static float volcanicDamage = 1f;
    public static float volcanicAttackSpeed = 0.1f;



    [Header("Radiation Boons")]
    public static bool contamination = false;
    public static float contaminationBonus = 1.5f;
    public static float falloutBonus = 12;
    public static bool finishHim = false;
    public static float finishHimBonus = 20;
    public static bool monteCarlo;
    public static float monteCarloChance = 2f;
    public static bool noMansLand = false;
    public static float noMansLandDamage = 5f;
    public static float noMansLandAttackSpeed = 0.35f;
    public static bool nuclear = false;
    public static float nuclearRegenerationSpeed = 0.2f;
    public static float nuclearRegeneration = 1f;
    public static bool radonBlood = false;
    public static float radonDamage = 10f;
    public static float radonAttackSpeed = 0.5f;
    public static float radonDuration = 4f;
    public static float radonRange = 4f;
    public static float wastelandBonus = 2.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
