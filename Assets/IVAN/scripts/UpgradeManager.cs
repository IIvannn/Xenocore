using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Slots")]
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    [Header("Weapon upgrades")]
    public List<UpgradeData> boomerangUpgrades = new List<UpgradeData>();

    [Header("Elemental upgrades")]
    public List<UpgradeData> swarmUpgrades = new List<UpgradeData>();
    public List<UpgradeData> hauntedUpgrades = new List<UpgradeData>();
    public List<UpgradeData> crystallizeUpgrades = new List<UpgradeData>();
    public List<UpgradeData> nullUpgrades = new List<UpgradeData>();
    public List<UpgradeData> starfallUpgrades = new List<UpgradeData>();
    public List<UpgradeData> rustUpgrades = new List<UpgradeData>();
    public List<UpgradeData> tectonicUpgrades = new List<UpgradeData>();
    public List<UpgradeData> radiationUpgrades = new List<UpgradeData>();


    List<UpgradeData> AvailableUpgrades = new List<UpgradeData>();
    List<UpgradeData> SelectedUpgrades = new List<UpgradeData>();
    public static List<UpgradeData> OwnedUpgrades = new List<UpgradeData>();

    int[] numbers;


    void Start()
    {
        
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
                foreach (var item in swarmUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "haunted":
                foreach (var item in hauntedUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "crystallize":
                foreach (var item in crystallizeUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "null":
                foreach (var item in nullUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "starfall":
                foreach (var item in starfallUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "rust":
                foreach (var item in rustUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "tectonic":
                foreach (var item in tectonicUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "radiation":
                foreach (var item in radiationUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;

        }

    }
    public void ChoseUpgrades()
    {
        //Debug.Log("Available upgrades at the start of the function:  "+AvailableUpgrades.Count);
        AvailableUpgrades.RemoveAll(item => OwnedUpgrades.Contains(item));
        numbers = Enumerable.Range(0, AvailableUpgrades.Count).ToArray(); // 0 to 10 inclusive
        
        //Debug.Log("Available upgrades after removing already selected ones:  "+AvailableUpgrades.Count);

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
        //Debug.Log("Available Upgrades after selection:  " + AvailableUpgrades.Count);
        //Debug.Log("Selected upgrades after reintroduction:  " + SelectedUpgrades.Count);
    }

    void ApplyUpgrade(string upg)
    {
        Debug.Log("apply upgrade:  "+upg);
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
                PlayerShoot.attackType = "swarm";
                return;
            case "swarm special":
                PlayerShoot.boomerangSpecialType = "swarm";
                return;

            //HAUNTED
            case "haunted attack":
                PlayerShoot.attackType = "haunted";
                return;
            case "haunted special":
                PlayerShoot.boomerangSpecialType = "haunted";
                return;

            //CRYSTALLIZE
            case "crystallize attack":
                PlayerShoot.attackType = "crystallize";
                return;
            case "crystallize special":
                PlayerShoot.boomerangSpecialType = "crystallize";
                return;

            //NULL
            case "null attack":
                PlayerShoot.attackType = "null";
                return;
            case "null special":
                PlayerShoot.boomerangSpecialType = "null";
                return;

            //STARFALL
            case "starfall attack":
                PlayerShoot.attackType = "starfall";
                return;
            case "starfall special":
                PlayerShoot.boomerangSpecialType = "starfall";
                return;

            //RUST
            case "rust attack":
                PlayerShoot.attackType = "rust";
                return;
            case "rust special":
                PlayerShoot.boomerangSpecialType = "rust";
                return;


            //TECTONIC
            case "tectonic attack":
                PlayerShoot.attackType = "tectonic";
                return;
            case "tectonic special":
                PlayerShoot.boomerangSpecialType = "tectonic";
                return;


            //RADIATION
            case "radiation attack":
                PlayerShoot.attackType = "radiation";
                return;
            case "radiation special":
                PlayerShoot.boomerangSpecialType = "radiation";
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
