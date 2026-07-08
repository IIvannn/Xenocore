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
    public static float starfallDamage = 50;
    public static float starfallChance = 25;
    public static float starfallDuration = 5;
    [Header("Rust")]
    public static float rustCritChance = 35f;
    public static float rustSelfDamage = 25;
    public static float rustSelfDamageChance = 40;
    public static float rustDuration = 5f;
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
    public static float doorHeal = 50f;
    public static  List<Transform> enemiesInRange = new List<Transform>();
    public static List<GameObject> enemiesAlive = new List<GameObject>();
    public static int UPGRADES = 0;
    public static float globalCritChance = 0;
    public static float spellCost = 100;
    public static float damageDone = 0;
    public static float highestDamage = 0;
    public static int essence = 0;



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
    public static int BoomerangUpgrades = 0;

    

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
    public static float nestDuration = 9f;
    public static float waspSpawnSpeed = 0.9f;
    public static float waspDamage = 10f;
    public static float waspSpeed = 10f;
    public static float proliferationBonus = 1.5f;
    public static float frenzyBonus = 0.15f;
    public static float swarmSpellCost = 130;


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
    public static float untouchableChance = 18f;
    public static float hauntedSpellDamage = 10f;
    public static float hauntedSpellRange = 6f;
    public static float hauntedSpellCost = 150;


    [Header("Crystallize Boons")]
    public static bool arcaneSwiftness = false;
    public static float arcaneSwiftnessBonus = 1.50f;
    public static float arcaneSwiftnessDuration = 5f;
    public static bool crystallineArmor = false;
    public static float crystallineArmorChance = 20f;
    public static bool energized = false;
    public static float energizedBonus = 5f;
    public static float excavationBonus = 0.20f;
    public static float fortuneBonus = 12f;
    public static bool medusa = false;
    public static float medusaChance = 45f;
    public static float petrifyDuration = 1.3f;
    public static bool monopoly = false;
    public static float momopolyBonus = 0.02f;
    public static bool overgrowth = false;
    public static float overgrowthChance = 10f;
    public static float prismDuration = 8f;
    public static float prismDamage = 12f;
    public static float prismRange = 8f;
    public static float prismCooldown = 1.2f;
    public static bool resonance = false;
    public static float resonanceDamage = 18f;
    public static float crystallizeSpellCost = 70;


    [Header("Null Boons")]
    public static bool farFar = false;
    public static float farFarBonus = 0.6f;
    public static bool collapse = false;
    public static float collapseDamage = 20f;
    public static bool exponentiallity = false;
    public static float exponentiallityBonus = 50f;
    public static float graspBonus = 3.5f;
    public static float gravityBonus = 0.5f;
    public static bool massAccumulation = false;
    public static float massAccumulationBonus = 1.5f;
    public static bool multiversalStrike = false;
    public static float multiversalStrikeDamage = 10;
    public static float multiversalStrikeCooldown = 2f;
    public static bool mstrike = true;
    public static bool pb = false;
    public static List<GameObject> nulledEnemies = new List<GameObject>();
    public static float nullSpellCost = 170;


    [Header("Starfall Boons")]
    public static float clearSkyBonus = 10f;
    public static bool crater = false;
    public static float craterRange = 2.5f;
    public static bool doubleTrouble = false;
    public static float doubleTroubleChance = 8;
    public static bool fated = false;
    public static float fatedBonus = 1.25f;
    public static bool luckyStar = false;
    public static float luckyStarChance = 50;
    public static float starfallCritChance = 0;
    public static float makeAWishCC = 25;
    public static float makeAWishCD = 1.3f;
    public static float meteoriteBonus = 15f;
    public static bool noYou = false;
    public static float noYouChance = 15f;
    public static float noYouRange = 15f;
    public static bool shenanigans = false;
    public static float starfallSpellCost = 120;


    [Header("Rust Boons")]
    public static bool cracked = false;
    public static float crackedBonus = 0.4f;
    public static bool embrittled = false;
    public static float embrittledChance = 40f;
    public static bool fatigue = false;
    public static float fatigueBonus = 15f;
    public static bool galvanized = false;
    public static float galvanizedBonus = 1.2f;
    public static float mutilationBonus = 20f;
    public static float oxidisedBonus = 20f;
    public static bool purge = false;
    public static bool soldering = false;
    public static float solderingBonus = 3f;
    public static bool tetanus = false;
    public static float tetanusChance = 10f;
    public static float rustSpellDuration = 6f;
    public static float rustSpellBonus = 30f;
    public static float rustSpellCost = 150;



    [Header("Tectonic Boons")]
    public static bool faceOff = false;
    public static float faceOffBonus = 25f;
    public static float faceOffRange = 10f;
    public static bool molten = false;
    public static float moltenBonus = 0.3f;
    public static bool mudbath = false;
    public static float mudbathChance = 6.25f;
    public static float mudbathMax = 15f;
    public static bool eruption = false;
    public static float eruptionRange = 8.5f;
    public static float eruptionDamage = 20f;
    public static float shatterBonus = 18f;
    public static bool tremble = false;
    public static float trembleChance = 50f;
    public static int staggerCap = 4;
    public static float staggerDuration = 1.5f;
    public static float tremorBonus = 1f;
    public static bool troglodite = false;
    public static bool volcanic = false;
    public static float volcanicChance = 35f;
    public static float volcanicDamage = 1f;
    public static float volcanicAttackSpeed = 0.1f;
    public static float tectonicSpellCost = 90;



    [Header("Radiation Boons")]
    public static bool contamination = false;
    public static float contaminationBonus = 0.35f;
    public static float falloutBonus = 12f;
    public static bool finishHim = false;
    public static float finishHimBonus = 30f;
    public static bool monteCarlo;
    public static float monteCarloChance = 5f;
    public static bool noMansLand = false;
    public static float noMansLandDamage = 5f;
    public static float noMansLandAttackSpeed = 0.35f;
    public static bool nuclear = false;
    public static float nuclearRegeneration = 1.5f;
    public static bool radonBlood = false;
    public static float radonDamage = 10f;
    public static float radonAttackSpeed = 0.5f;
    public static float radonDuration = 4f;
    public static float radonRange = 4f;
    public static float wastelandBonus = 2.5f;
    public static float radiationSpellCost = 140;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void RESETUPGRADES()
    {
        PlayerDamage.hp = 100;
        PlayerShoot.attackType = "normal";
        PlayerShoot.boomerangSpecialType = "normal";
        PlayerShoot.spellType = "normal";



        swarmDamage = 4f;
        swarmAttackSpeed = 0.4f;
        swarmRange = 3f;
        swarmDuration = 5f;

        hauntedInitialDamage = 30f;
        hauntedDamagePercentage = 40f;
        hauntedDuration = 3f;
        hauntedGhostSpeed = 5f;

        crystallizeCrystalChance = 25f;
        crystallizeCrystalAmmount = 1;
        crystallizeEnergyAmmount = 1.5f;
        crystallizeDuration = 6f;

        nullRange = 10f;
        nullPullStrength = 1.8f;
        nullDuration = 3f;
        nullMaxCount = 3;
        nullCurrentCount = 0;

        starfallDamage = 50;
        starfallChance = 25;
        starfallDuration = 5;

        rustCritChance = 35f;
        rustSelfDamage = 25;
        rustSelfDamageChance = 40;
        rustDuration = 5f;

        tectonicDamage = 50f;
        tectonicAttackSpeed = 2.6f;
        tectonicRange = 2f;
        tectonicDuration = 7.8f;
        tectonicMaxCount = 2;
        tectonicCurrentCount = 0;
        tectonicSpread = 1.5f;
        tectonicSpreadSpeed = 0.6f;

        radiationWeakness = 22;
        radiationRange = 7f;
        radiationDuration = 8f;
        radiationMaxCount = 4;
        radiationCurrentCount = 0;



        crystals = 0;


        healingMultiplier = 1f;
        moneyMultiplier = 1f;
        doorHeal = 5f;
        enemiesInRange.Clear();
        enemiesAlive.Clear();
        UPGRADES = 0;
        globalCritChance = 0;
        spellCost = 100;

        hasAttack = false;
        hasSpecial = false;
        hasSpell = false;

        hasSwarm = false;
        hasHaunted = false;
        hasCrystallize = false;
        hasNull = false;
        hasStarfall = false;
        hasRust = false;
        hasTectonic = false;
        hasRadiation = false;

        boomerangGrow = false;
        boomerangGrowBonus = 0.2f;
        boomerangMark = false;
        boomerangReturnShockwave = false;
        boomerangSpeed = false;
        boomerangSpeedBonus = 15f;
        boomerangSpecialSpeedBonus = 250f;
        boomerangSpecialGrow = false;
        boomerangSpecialGrowBonus = 8f;
        boomerangSpecialCooldwon = false;
        BoomerangUpgrades = 0;

        silky = false;
        silkyBonus = 0.3f;
        adaptation = false;
        adaptationLimit = 0.3f;
        corrosive = false;
        corrosiveBonus = 0.5f;
        infestation = false;
        infestationChance = 35f;
        nested = false;
        nestedChance = 15f;
        pob = false;
        nestDuration = 9f;
        waspSpawnSpeed = 1f;
        waspDamage = 10f;
        waspSpeed = 10f;
        proliferationBonus = 1.5f;
        frenzyBonus = 0.15f;
        swarmSpellCost = 130;

        dreadBonus = 15f;
        emotionalDamage = false;
        emotionalDamageBonus = 0.2f;
        exorcism = false;
        exorcismBonus = 10f;
        jumpscareBonus = 35f;
        necromancer = false;
        necroancerChance = 15f;
        phaseDash = false;
        phaseDashDashDamage = 20f;
        phaseDashDashDurationBonus = 0.2f;
        posession = false;
        possessionChance = 5f;
        reaper = false;
        reaperBonus = 0;
        untouchable = false;
        untouchableChance = 18f;
        hauntedSpellDamage = 10f;
        hauntedSpellRange = 6f;
        hauntedSpellCost = 150;

        arcaneSwiftness = false;
        arcaneSwiftnessBonus = 1.50f;
        arcaneSwiftnessDuration = 5f;
        crystallineArmor = false;
        crystallineArmorChance = 20f;
        energized = false;
        energizedBonus = 5f;
        excavationBonus = 0.20f;
        fortuneBonus = 12f;
        medusa = false;
        medusaChance = 45f;
        petrifyDuration = 1.3f;
        monopoly = false;
        momopolyBonus = 0.02f;
        overgrowth = false;
        overgrowthChance = 10f;
        prismDuration = 8f;
        prismDamage = 12f;
        prismRange = 8f;
        prismCooldown = 1.2f;
        resonance = false;
        resonanceDamage = 18f;
        crystallizeSpellCost = 70;

        farFar = false;
        farFarBonus = 0.6f;
        collapse = false;
        collapseDamage = 20f;
        exponentiallity = false;
        exponentiallityBonus = 50f;
        graspBonus = 3.5f;
        gravityBonus = 0.5f;
        massAccumulation = false;
        massAccumulationBonus = 3f;
        multiversalStrike = false;
        multiversalStrikeDamage = 10;
        multiversalStrikeCooldown = 2f;
        mstrike = true;
        pb = false;
        nulledEnemies.Clear();
        nullSpellCost = 170;

        clearSkyBonus = 10f;
        crater = false;
        craterRange = 2.5f;
        doubleTrouble = false;
        doubleTroubleChance = 8;
        fated = false;
        fatedBonus = 1.25f;
        luckyStar = false;
        luckyStarChance = 50;
        starfallCritChance = 0;
        makeAWishCC = 25;
        makeAWishCD = 1.3f;
        meteoriteBonus = 15f;
        noYou = false;
        noYouChance = 15f;
        noYouRange = 15f;
        shenanigans = false;
        starfallSpellCost = 120;

        cracked = false;
        crackedBonus = 0.4f;
        embrittled = false;
        embrittledChance = 40f;
        fatigue = false;
        fatigueBonus = 15f;
        galvanized = false;
        galvanizedBonus = 1.2f;
        mutilationBonus = 20f;
        oxidisedBonus = 20f;
        purge = false;
        soldering = false;
        solderingBonus = 3f;
        tetanus = false;
        tetanusChance = 10f;
        rustSpellDuration = 6f;
        rustSpellBonus = 30f;
        rustSpellCost = 150;

        faceOff = false;
        faceOffBonus = 25f;
        faceOffRange = 10f;
        molten = false;
        moltenBonus = 0.3f;
        mudbath = false;
        mudbathChance = 6.25f;
        mudbathMax = 15f;
        eruption = false;
        eruptionRange = 8.5f;
        eruptionDamage = 20f;
        shatterBonus = 18f;
        tremble = false;
        trembleChance = 50f;
        staggerCap = 4;
        staggerDuration = 1.5f;
        tremorBonus = 1f;
        troglodite = false;
        volcanic = false;
        volcanicChance = 35f;
        volcanicDamage = 1f;
        volcanicAttackSpeed = 0.1f;
        tectonicSpellCost = 90;

        contamination = false;
        contaminationBonus = 0.35f;
        falloutBonus = 12f;
        finishHim = false;
        finishHimBonus = 30f;
        monteCarlo = false;
        monteCarloChance = 5f;
        noMansLand = false;
        noMansLandDamage = 5f;
        noMansLandAttackSpeed = 0.35f;
        nuclear = false;
        nuclearRegeneration = 1.5f;
        radonBlood = false;
        radonDamage = 10f;
        radonAttackSpeed = 0.5f;
        radonDuration = 4f;
        radonRange = 4f;
        wastelandBonus = 2.5f;
        radiationSpellCost = 140;

    }
}
