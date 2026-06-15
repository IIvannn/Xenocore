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


    bool hasAttack = false;
    bool hasSpecial = false;
    bool hasSpell = false;

    bool hasSwarm = false;
    bool hasHaunted = false;
    bool hasCrystallize = false;
    bool hasNull = false;
    bool hasStarfall = false;
    bool hasRust = false;
    bool hasTectonic = false;
    bool hasRadiation = false;


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
                foreach (var item in boomerangUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "crystallize":
                foreach (var item in boomerangUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "null":
                foreach (var item in boomerangUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "starfall":
                foreach (var item in boomerangUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "rust":
                foreach (var item in boomerangUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "tectonic":
                foreach (var item in boomerangUpgrades)
                {
                    if (!OwnedUpgrades.Contains(item))
                    {
                        AvailableUpgrades.Add(item);
                    }
                }
                return;
            case "radiation":
                foreach (var item in boomerangUpgrades)
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
        switch (upg)
        {
            case "boomerang grow":
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
