using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Slots")]
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    [Header("Upgrade categories")]
    public List<UpgradeData> boomerangUpgrades = new List<UpgradeData>();
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


    public static List<UpgradeData> AvailableUpgrades = new List<UpgradeData>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AvailableUpgrades.AddRange(boomerangUpgrades);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChoseUpgrades()
    {
        
        int[] numbers;
        numbers = Enumerable.Range(0, AvailableUpgrades.Count).ToArray();


        for (int i = numbers.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
        }
        Debug.Log(numbers[1]);

        

    }

    public static void UpgradeSelected(string upg)
    {
        switch (upg)
        {
            case "boomerang grow":
                return;
        }

    }
}
