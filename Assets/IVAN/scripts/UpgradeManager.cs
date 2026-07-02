using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeManager : MonoBehaviour
{
    [Header("Slots")]
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    [Header("Weapon upgrades")]
    public List<UpgradeData> boomerangUpgrades = new List<UpgradeData>();

    [Header("Swarm upgrades")]
    public List<UpgradeData> swarmUpgrades = new List<UpgradeData>();
    public List<UpgradeData> swarmyUpgrades = new List<UpgradeData>();
    public List<UpgradeData> swarmAttack = new List<UpgradeData>();
    public List<UpgradeData> swarmSpecial = new List<UpgradeData>();
    public List<UpgradeData> swarmSpell = new List<UpgradeData>();
    

    [Header("Haunted upgrades")]
    public List<UpgradeData> hauntedyUpgrades = new List<UpgradeData>();
    public List<UpgradeData> hauntedAttack = new List<UpgradeData>();
    public List<UpgradeData> hauntedSpecial = new List<UpgradeData>();
    public List<UpgradeData> hauntedSpell = new List<UpgradeData>();
    public List<UpgradeData> hauntedUpgrades = new List<UpgradeData>();
    

    [Header("Crystallize upgrades")]
    public List<UpgradeData> crystallizeyUpgrades = new List<UpgradeData>();
    public List<UpgradeData> crystallizeAttack = new List<UpgradeData>();
    public List<UpgradeData> crystallizeSpecial = new List<UpgradeData>();
    public List<UpgradeData> crystallizeSpell = new List<UpgradeData>();
    public List<UpgradeData> crystallizeUpgrades = new List<UpgradeData>();
    

    [Header("Null upgrades")]
    public List<UpgradeData> nullyUpgrades = new List<UpgradeData>();
    public List<UpgradeData> nullAttack = new List<UpgradeData>();
    public List<UpgradeData> nullSpecial = new List<UpgradeData>();
    public List<UpgradeData> nullSpell = new List<UpgradeData>();
    public List<UpgradeData> nullUpgrades = new List<UpgradeData>();

    [Header("Starfall upgrades")]
    public List<UpgradeData> starfallUpgrades = new List<UpgradeData>();
    public List<UpgradeData> starfallAttack = new List<UpgradeData>();
    public List<UpgradeData> starfallSpecial = new List<UpgradeData>();
    public List<UpgradeData> starfallSpell = new List<UpgradeData>();
    public List<UpgradeData> starfallyUpgrades = new List<UpgradeData>();

    [Header("Rust upgrades")]
    public List<UpgradeData> rustUpgrades = new List<UpgradeData>();
    public List<UpgradeData> rustAttack = new List<UpgradeData>();
    public List<UpgradeData> rustSpecial = new List<UpgradeData>();
    public List<UpgradeData> rustSpell = new List<UpgradeData>();
    public List<UpgradeData> rustyUpgrades = new List<UpgradeData>();

    [Header("Tectonic upgrades")]
    public List<UpgradeData> tectonicUpgrades = new List<UpgradeData>();
    public List<UpgradeData> tectonicAttack = new List<UpgradeData>();
    public List<UpgradeData> tectonicSpecial = new List<UpgradeData>();
    public List<UpgradeData> tectonicSpell = new List<UpgradeData>();
    public List<UpgradeData> tectonicyUpgrades = new List<UpgradeData>();

    [Header("Radation upgrades")]
    public List<UpgradeData> radiationUpgrades = new List<UpgradeData>();
    public List<UpgradeData> radiationAttack = new List<UpgradeData>();
    public List<UpgradeData> radiationSpecial = new List<UpgradeData>();
    public List<UpgradeData> radiationSpell = new List<UpgradeData>();
    public List<UpgradeData> radiationyUpgrades = new List<UpgradeData>();



    List<UpgradeData> AvailableUpgrades = new List<UpgradeData>();
    List<UpgradeData> SelectedUpgrades = new List<UpgradeData>();
    public static List<UpgradeData> OwnedUpgrades = new List<UpgradeData>();

    int[] numbers;


    void Start()
    {
        
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            gameObject.SetActive(false);
        }
    }

    public void Upgrader(string type)
    {
        ChooseType(type);
        ChoseUpgrades();
        
    }

    public void ChooseType(string type)
    {
        switch(type)
        {
            case "boomerang":
                foreach (var item in boomerangUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "swarm":
                if (BoonSTaticInfo.hasSwarm)
                {
                    foreach (var item in swarmyUpgrades)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                foreach (var item in swarmUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }

                if (!BoonSTaticInfo.hasAttack)
                {
                    Debug.Log("Attack added");
                    foreach (var item in swarmAttack)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpecial)
                {
                    foreach (var item in swarmSpecial)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpell)
                {
                    foreach (var item in swarmSpell)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }

                return;
            case "haunted":
                if (BoonSTaticInfo.hasHaunted)
                {
                    foreach (var item in hauntedyUpgrades)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                foreach (var item in hauntedUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }

                if (!BoonSTaticInfo.hasAttack)
                {
                    Debug.Log("Attack added");
                    foreach (var item in hauntedAttack)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpecial)
                {
                    foreach (var item in hauntedSpecial)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpell)
                {
                    foreach (var item in hauntedSpell)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }

                return;
            case "crystallize":
                if (BoonSTaticInfo.hasCrystallize)
                {
                    foreach (var item in crystallizeyUpgrades)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                foreach (var item in crystallizeUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }

                if (!BoonSTaticInfo.hasAttack)
                {
                    Debug.Log("Attack added");
                    foreach (var item in crystallizeAttack)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpecial)
                {
                    foreach (var item in crystallizeSpecial)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpell)
                {
                    foreach (var item in crystallizeSpell)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }

                return;
            case "null":
                if (BoonSTaticInfo.hasNull)
                {
                    foreach (var item in nullyUpgrades)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                foreach (var item in nullUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }

                if (!BoonSTaticInfo.hasAttack)
                {
                    Debug.Log("Attack added");
                    foreach (var item in nullAttack)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpecial)
                {
                    foreach (var item in nullSpecial)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpell)
                {
                    foreach (var item in nullSpell)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }

                return;
            case "starfall":
                if (BoonSTaticInfo.hasStarfall)
                {
                    foreach (var item in starfallyUpgrades)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                foreach (var item in starfallUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }

                if (!BoonSTaticInfo.hasAttack)
                {
                    Debug.Log("Attack added");
                    foreach (var item in starfallAttack)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpecial)
                {
                    foreach (var item in starfallSpecial)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpell)
                {
                    foreach (var item in starfallSpell)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }

                return;
            case "rust":
                if (BoonSTaticInfo.hasRust)
                {
                    foreach (var item in rustyUpgrades)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                foreach (var item in rustUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }

                if (!BoonSTaticInfo.hasAttack)
                {
                    Debug.Log("Attack added");
                    foreach (var item in rustAttack)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpecial)
                {
                    foreach (var item in rustSpecial)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpell)
                {
                    foreach (var item in rustSpell)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }

                return;
            case "tectonic":
                if (BoonSTaticInfo.hasTectonic)
                {
                    foreach (var item in tectonicyUpgrades)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                foreach (var item in tectonicUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }

                if (!BoonSTaticInfo.hasAttack)
                {
                    Debug.Log("Attack added");
                    foreach (var item in tectonicAttack)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpecial)
                {
                    foreach (var item in tectonicSpecial)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpell)
                {
                    foreach (var item in tectonicSpell)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }

                return;
            case "radiation":
                if (BoonSTaticInfo.hasRadiation)
                {
                    foreach (var item in radiationyUpgrades)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                foreach (var item in radiationUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }

                if (!BoonSTaticInfo.hasAttack)
                {
                    Debug.Log("Attack added");
                    foreach (var item in radiationAttack)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpecial)
                {
                    foreach (var item in radiationSpecial)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }
                if (!BoonSTaticInfo.hasSpell)
                {
                    foreach (var item in radiationSpell)
                    {
                        if (!OwnedUpgrades.Contains(item))
                        {
                            AvailableUpgrades.Add(item);
                        }
                    }
                }

                return;

        }

    }
    public void ChoseUpgrades()
    {
        slot1.SetActive(true);
        slot2.SetActive(true);
        slot3.SetActive(true);

        //Debug.Log("Available upgrades at the start of the function:  "+AvailableUpgrades.Count);
        AvailableUpgrades.RemoveAll(item => OwnedUpgrades.Contains(item));
        numbers = Enumerable.Range(0, AvailableUpgrades.Count).ToArray(); // 0 to 10 inclusive
        
        Debug.Log("Available upgrades after removing already selected ones:  "+AvailableUpgrades.Count);

        //Debug.Log(string.Join(", ", numbers));

        for (int i = numbers.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
        }

        slot1.GetComponent<UpgradeSlot>().ud = null;
        slot2.GetComponent<UpgradeSlot>().ud = null;
        slot3.GetComponent<UpgradeSlot>().ud = null;

        if (AvailableUpgrades.Count > 0)
        {
            
            slot1.GetComponent<UpgradeSlot>().ud = AvailableUpgrades[numbers[0]];
            SelectedUpgrades.Add(AvailableUpgrades[numbers[0]]);
        }
        if (AvailableUpgrades.Count > 1)
        {
            
            slot2.GetComponent<UpgradeSlot>().ud = AvailableUpgrades[numbers[1]];
            SelectedUpgrades.Add(AvailableUpgrades[numbers[1]]);
        }
        if (AvailableUpgrades.Count > 2)
        {
            
            slot3.GetComponent<UpgradeSlot>().ud = AvailableUpgrades[numbers[2]];
            SelectedUpgrades.Add(AvailableUpgrades[numbers[2]]);
        }

        slot1.GetComponent<UpgradeSlot>().AttributesSet();
        slot2.GetComponent<UpgradeSlot>().AttributesSet();
        slot3.GetComponent<UpgradeSlot>().AttributesSet();

        AvailableUpgrades.RemoveAll(item => SelectedUpgrades.Contains(item));
        //Debug.Log("Available Upgrades at the end of the function:  "+AvailableUpgrades.Count);

    }

    public void UpgradeSelected(string upg, int slot)
    {
        Reintroduce(slot);
        ApplyUpgrade(upg);
        gameObject.SetActive(false);
        AvailableUpgrades.Clear();
        Debug.Log("Available Upgrades after selection:  " + AvailableUpgrades.Count);
        //Debug.Log("Selected upgrades after reintroduction:  " + SelectedUpgrades.Count);
    }

    void ApplyUpgrade(string upg)
    {
        Debug.Log("apply upgrade:  "+upg);
        BoonSTaticInfo.UPGRADES++;
        switch (upg)
        {
            //BOOMERANG
            case "boomerang grow":
                BoonSTaticInfo.boomerangGrow = true;
                return;
            case "boomerang mark":
                BoonSTaticInfo.boomerangMark = true;
                return;
            case "boomerang return shockwave":
                BoonSTaticInfo.boomerangReturnShockwave = true;
                return;
            case "boomerang speed":
                BoonSTaticInfo.boomerangSpeed = true;
                return;
            case "boomerang special cooldown":
                BoonSTaticInfo.boomerangSpecialCooldwon = true;
                return;
            case "boomerang special grow":
                BoonSTaticInfo.boomerangSpecialGrow = true;
                return;

            //SWARM
            case "swarm attack":
                PlayerShoot.attackType = "swarm"; //DONE
                BoonSTaticInfo.hasAttack = true;
                BoonSTaticInfo.hasSwarm = true;
                return;
            case "swarm special":
                PlayerShoot.boomerangSpecialType = "swarm"; //DONE
                BoonSTaticInfo.hasSpecial = true;
                BoonSTaticInfo.hasSwarm = true;
                return;
            case "swarm spell":
                BoonSTaticInfo.hasSpell = true; //TBD
                PlayerShoot.spellType = "swarm";
                BoonSTaticInfo.spellCost = BoonSTaticInfo.swarmSpellCost;
                return;
            case "proliferation":
                BoonSTaticInfo.swarmRange += BoonSTaticInfo.proliferationBonus; //DONE
                return;
            case "adaptation":
                BoonSTaticInfo.adaptation = true; //DONE
                return;
            case "corrosive":
                BoonSTaticInfo.corrosive = true; //DONE
                return;
            case "feast":
                BoonSTaticInfo.healingMultiplier += BoonSTaticInfo.frenzyBonus; //DONE
                return;
            case "frenzy":
                BoonSTaticInfo.swarmAttackSpeed -= BoonSTaticInfo.frenzyBonus; //DONE
                return;
            case "infestation":
                BoonSTaticInfo.infestation = true; //DONE
                return;
            case "nested":
                BoonSTaticInfo.nested = true; //DONE
                return;
            case "path of the bug":
                BoonSTaticInfo.pob = true; //DONE
                return;
            case "silky":
                BoonSTaticInfo.silky = true; //DONE
                return;
            

            //HAUNTED
            case "haunted attack":
                BoonSTaticInfo.hasAttack = true;
                PlayerShoot.attackType = "haunted";
                BoonSTaticInfo.hasHaunted = true; //DONE
                return;
            case "haunted special":
                BoonSTaticInfo.hasSpecial = true;
                BoonSTaticInfo.hasHaunted = true;
                PlayerShoot.boomerangSpecialType = "haunted"; //DONE
                return;
            case "haunted spell":
                PlayerShoot.spellType = "haunted";
                BoonSTaticInfo.hasSpell = true; //TBD
                BoonSTaticInfo.spellCost = BoonSTaticInfo.hauntedSpellCost;
                return;
            case "reaper":
                BoonSTaticInfo.reaper = true; //DONE
                return;
            case "untouchable":
                BoonSTaticInfo.untouchable = true; //DONE
                return;
            case "phase dash":
                BoonSTaticInfo.phaseDash = true; //DONE
                return;
            case "dread":
                BoonSTaticInfo.hauntedDamagePercentage += BoonSTaticInfo.dreadBonus; //DONE
                return;
            case "jumpscare":
                BoonSTaticInfo.hauntedInitialDamage += BoonSTaticInfo.jumpscareBonus; //DONE
                return;
            case "possession":
                BoonSTaticInfo.posession = true; //DONE
                return;
            case "emotional damage":
                BoonSTaticInfo.emotionalDamage = true; //DONE
                return;
            case "necromancer":
                BoonSTaticInfo.necromancer = true; //DONE
                return;
            case "exorcism":
                BoonSTaticInfo.exorcism = true; //DONE
                return;


            //CRYSTALLIZE
            case "crystallize attack":
                BoonSTaticInfo.hasAttack = true;
                BoonSTaticInfo.hasCrystallize = true;
                PlayerShoot.attackType = "crystallize"; //DONE
                return;
            case "crystallize special":
                BoonSTaticInfo.hasSpecial = true;
                BoonSTaticInfo.hasCrystallize = true;
                PlayerShoot.boomerangSpecialType = "crystallize"; //DONE
                return;
            case "crystallize spell":
                BoonSTaticInfo.hasSpell = true; //TBD
                PlayerShoot.spellType = "crystallize";
                BoonSTaticInfo.spellCost = BoonSTaticInfo.crystallizeSpellCost;
                return;
            case "arcane swiftness":
                BoonSTaticInfo.arcaneSwiftness = true; //DONE
                return;
            case "resonance":
                BoonSTaticInfo.resonance = true; //DONE
                return;
            case "crystalline armor":
                BoonSTaticInfo.crystallineArmor = true; //DONE
                return;
            case "energized":
                BoonSTaticInfo.energized = true; //DONE
                return;
            case "excavator":
                BoonSTaticInfo.moneyMultiplier += BoonSTaticInfo.excavationBonus; //DONE
                return;
            case "fortune":
                BoonSTaticInfo.crystallizeCrystalChance += BoonSTaticInfo.fortuneBonus; //DONE
                return;
            case "medusa":
                BoonSTaticInfo.medusa = true; //DONE
                return;
            case "monopoly":
                BoonSTaticInfo.monopoly = true; //DONE
                return;
            case "overgrowth":
                BoonSTaticInfo.overgrowth = true; //DONE
                return;


            //NULL
            case "null attack":
                BoonSTaticInfo.hasAttack = true;
                BoonSTaticInfo.hasNull = true;
                PlayerShoot.attackType = "null"; //DONE
                return;
            case "null special":
                BoonSTaticInfo.hasSpecial = true;
                BoonSTaticInfo.hasNull = true;
                PlayerShoot.boomerangSpecialType = "null"; //DONE
                return;
            case "null spell":
                BoonSTaticInfo.hasSpell = true; //TBD
                PlayerShoot.spellType = "null";
                BoonSTaticInfo.spellCost = BoonSTaticInfo.nullSpellCost;
                return;
            case "a long time ago in a galaxy far far away":
                BoonSTaticInfo.farFar = true; //DONE
                return;
            case "collapse":
                BoonSTaticInfo.collapse = true; //DONE
                return;
            case "exponentiality":
                BoonSTaticInfo.exponentiallity = true; //DONE
                return;
            case "grasp":
                BoonSTaticInfo.nullRange += BoonSTaticInfo.graspBonus; //DONE
                return;
            case "gravity":
                BoonSTaticInfo.nullPullStrength += BoonSTaticInfo.gravityBonus; //DONE
                return;
            case "into the void":
                BoonSTaticInfo.nullMaxCount++; //DONE
                return;
            case "mass accumulation":
                BoonSTaticInfo.massAccumulation = true; //DONE
                return;
            case "multiversal strike":
                BoonSTaticInfo.multiversalStrike = true; //DONE
                return;
            case "pocket black hole":
                BoonSTaticInfo.pb = true; //DONE
                return;


            //STARFALL
            case "starfall attack":
                BoonSTaticInfo.hasAttack = true;
                BoonSTaticInfo.hasStarfall = true;
                PlayerShoot.attackType = "starfall"; //DONE
                return;
            case "starfall special":
                BoonSTaticInfo.hasSpecial = true;
                BoonSTaticInfo.hasStarfall = true;
                PlayerShoot.boomerangSpecialType = "starfall"; //DONE
                return;
            case "starfall spell":
                PlayerShoot.spellType = "starfall";
                BoonSTaticInfo.hasSpell = true; //TBD
                BoonSTaticInfo.spellCost = BoonSTaticInfo.starfallSpellCost;
                return;
            case "clear sky":
                BoonSTaticInfo.starfallChance += BoonSTaticInfo.clearSkyBonus; //DONE
                return;
            case "crater":
                BoonSTaticInfo.crater = true; //DONE
                return;
            case "double trouble":
                BoonSTaticInfo.doubleTrouble = true; //DONE
                return;
            case "fated":
                BoonSTaticInfo.fated = true; //DONE
                return;
            case "lucky star":
                BoonSTaticInfo.luckyStar = true; //DONE
                return;
            case "make a wish":
                BoonSTaticInfo.starfallCritChance += BoonSTaticInfo.makeAWishCC; //DONE
                return;
            case "meteorite":
                BoonSTaticInfo.starfallDamage += BoonSTaticInfo.meteoriteBonus; //DONE
                return;
            case "no you":
                BoonSTaticInfo.noYou = true; //DONE
                return;
            case "shenanigans":
                BoonSTaticInfo.shenanigans = true; //DONE
                return;


            //RUST
            case "rust attack":
                BoonSTaticInfo.hasAttack = true;
                BoonSTaticInfo.hasRust = true;
                PlayerShoot.attackType = "rust"; //DONE
                return;
            case "rust special":
                BoonSTaticInfo.hasSpecial = true;
                BoonSTaticInfo.hasRust = true;
                PlayerShoot.boomerangSpecialType = "rust"; //DONE
                return;
            case "rust spell":
                PlayerShoot.spellType = "rust";
                BoonSTaticInfo.hasSpell = true; //TBD
                BoonSTaticInfo.spellCost = BoonSTaticInfo.rustSpellCost;
                return;
            case "cracked":
                BoonSTaticInfo.cracked = true; //DONE
                return;
            case "embrittled":
                BoonSTaticInfo.embrittled = true; //DONE
                return;
            case "fatigue":
                BoonSTaticInfo.fatigue = true; //DONE
                return;
            case "galvanized":
                BoonSTaticInfo.galvanized = true; //DONE
                return;
            case "mutilation":
                BoonSTaticInfo.rustSelfDamage += BoonSTaticInfo.mutilationBonus; //DONE
                return;
            case "oxidised":
                BoonSTaticInfo.rustCritChance += BoonSTaticInfo.oxidisedBonus; //DONE
                return;
            case "purge":
                BoonSTaticInfo.purge = true;
                PlayerDamage.hp -= (PlayerDamage.hp / 2); //DONE
                return;
            case "soldering":
                BoonSTaticInfo.soldering = true; //DONE
                return;
            case "tetanus":
                BoonSTaticInfo.tetanus = true; //DONE
                return;



            //TECTONIC
            case "tectonic attack":
                BoonSTaticInfo.hasAttack = true;
                BoonSTaticInfo.hasTectonic = true;
                PlayerShoot.attackType = "tectonic"; //DONE
                return;
            case "tectonic special":
                BoonSTaticInfo.hasSpecial = true;
                BoonSTaticInfo.hasTectonic = true;
                PlayerShoot.boomerangSpecialType = "tectonic"; //DONE
                return;
            case "tectonic spell":
                BoonSTaticInfo.hasSpell = true; //TBD
                PlayerShoot.spellType = "tectonic";
                BoonSTaticInfo.spellCost = BoonSTaticInfo.tectonicSpellCost;
                return;
            case "face off":
                BoonSTaticInfo.faceOff = true; //DONE
                return;
            case "molten":
                BoonSTaticInfo.molten = true; //DONE
                return;
            case "mudbath":
                BoonSTaticInfo.mudbath = true; //TBD
                return;
            case "eruption":
                BoonSTaticInfo.eruption = true; //DONE
                return;
            case "shatter":
                BoonSTaticInfo.tectonicDamage += BoonSTaticInfo.shatterBonus; //DONE
                return;
            case "tremble":
                BoonSTaticInfo.tremble = true; //DONE
                return;
            case "tremor":
                BoonSTaticInfo.tectonicSpread += BoonSTaticInfo.tremorBonus; //DONE
                return;
            case "troglodite":
                BoonSTaticInfo.troglodite = true; //DONE
                return;
            case "volcanic":
                BoonSTaticInfo.volcanic = true; //DONE
                return;


            //RADIATION
            case "radiation attack":
                BoonSTaticInfo.hasAttack = true;
                BoonSTaticInfo.hasRadiation = true;
                PlayerShoot.attackType = "radiation"; //DONE
                return;
            case "radiation special":
                BoonSTaticInfo.hasSpecial = true;
                BoonSTaticInfo.hasRadiation = true;
                PlayerShoot.boomerangSpecialType = "radiation"; //DONE
                return;
            case "radiation spell":
                BoonSTaticInfo.hasSpell = true; //TBD
                PlayerShoot.spellType = "radiation";
                BoonSTaticInfo.spellCost = BoonSTaticInfo.radiationSpellCost;
                return;
            case "armagedon":
                BoonSTaticInfo.radiationMaxCount ++; //DONE
                return;
            case "contamination":
                BoonSTaticInfo.contamination = true; //DONE
                return;
            case "fallout":
                BoonSTaticInfo.radiationWeakness += BoonSTaticInfo.falloutBonus; //DONE
                return;
            case "finish him":
                BoonSTaticInfo.finishHim = true; //DONE
                return;
            case "monte carlo":
                BoonSTaticInfo.monteCarlo = true; //DONE
                return;
            case "no mans land":
                BoonSTaticInfo.noMansLand = true; //DONE
                return;
            case "nuclear":
                BoonSTaticInfo.nuclear = true; //DONE
                return;
            case "radon blood":
                BoonSTaticInfo.radonBlood = true; //TBD
                return;
            case "wasteland":
                BoonSTaticInfo.radiationRange += BoonSTaticInfo.wastelandBonus; //DONE
                return;

        }
    }

    void Reintroduce(int slot)
    {
        //Debug.Log("Selected upgrades before reintroduction:  "+SelectedUpgrades.Count);
        switch (slot)
        {
            case 0:
                OwnedUpgrades.Add(SelectedUpgrades[0]);
                if (SelectedUpgrades.Count == 2)
                {
                    AvailableUpgrades.Add(SelectedUpgrades[1]);
                }
                if (SelectedUpgrades.Count == 3)
                {
                    AvailableUpgrades.Add(SelectedUpgrades[2]);
                }
                SelectedUpgrades.Clear();
                return;
            case 1:
                OwnedUpgrades.Add(SelectedUpgrades[1]);
                if (SelectedUpgrades.Count == 2)
                {
                    AvailableUpgrades.Add(SelectedUpgrades[0]);
                }
                if (SelectedUpgrades.Count == 3)
                {
                    AvailableUpgrades.Add(SelectedUpgrades[2]);
                }
                SelectedUpgrades.Clear();
                return;
            case 2:
                OwnedUpgrades.Add(SelectedUpgrades[2]);
                if (SelectedUpgrades.Count == 2)
                {
                    AvailableUpgrades.Add(SelectedUpgrades[1]);
                }
                if (SelectedUpgrades.Count == 3)
                {
                    AvailableUpgrades.Add(SelectedUpgrades[0]);
                }
                SelectedUpgrades.Clear();
                return;
        }
    }
}
