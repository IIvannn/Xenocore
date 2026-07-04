using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Waver : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> wave1 = new List<GameObject>();
    public List<GameObject> wave2 = new List<GameObject>();
    public List<GameObject> wave3 = new List<GameObject>();
    public List<GameObject> rewardPool = new List<GameObject>();
    public int waves = 0;
    public float timebeforeStart = 3;
    public float summonSpeed = 0.3f;
    bool finished = false;
    bool finishedFR = false;

    public GameObject rewardPosition;
    GameObject reward;
    public GameObject finalReward;


    public static bool roomEnded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rewardPosition.SetActive(false);
        StartCoroutine(StartWaves());
        waves++;

        switch (DoorScript.currentRoom)
        {
            case 1:
                ChooseReward(DoorScript.selectedRewards[0]);
                return;
            case 2:
                ChooseReward(DoorScript.selectedRewards[1]);
                return;
            case 3:
                ChooseReward(DoorScript.selectedRewards[2]);
                return;
            case 4:
                ChooseReward(DoorScript.selectedRewards[3]);
                return;
            case 5:
                ChooseReward(DoorScript.selectedRewards[4]);
                return;
            case 6:
                ChooseReward(DoorScript.selectedRewards[5]);
                return;
            case 7:
                ChooseReward(DoorScript.selectedRewards[6]);
                return;
            case 8:
                ChooseReward(DoorScript.selectedRewards[7]);
                return;
            case 9:
                ChooseReward(DoorScript.selectedRewards[8]);
                return;
            case 10:
                ChooseReward(DoorScript.selectedRewards[9]);
                return;
            case 11:
                ChooseReward(DoorScript.selectedRewards[10]);
                return;
            case 12:
                ChooseReward(DoorScript.selectedRewards[11]);
                return;
        }
    }


    void ChooseReward(string type)
    {
        Debug.Log("type of upgrade:  "+type);
        int relement = Random.Range(1, DoorScript.selectedElements.Count);
        switch (type)
        {
            case "upgrade":
                
                switch (DoorScript.selectedElements[relement])
                {
                    case "swarm":
                        reward = rewardPool[0];
                        Debug.Log(DoorScript.selectedElements[relement]);

                        GameObject swa = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                        if (swa != null)
                        {
                            finalReward = swa;
                            finalReward.SetActive(false);
                        }
                        return;
                    case "haunted":
                        reward = rewardPool[1];
                        Debug.Log(DoorScript.selectedElements[relement]);

                        GameObject hau = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                        if (hau != null)
                        {
                            finalReward = hau;
                            finalReward.SetActive(false);
                        }
                        return;
                    case "crystallize":
                        reward = rewardPool[2];
                        Debug.Log(DoorScript.selectedElements[relement]);

                        GameObject cry = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                        if (cry != null)
                        {
                            finalReward = cry;
                            finalReward.SetActive(false);
                        }
                        return;
                    case "null":
                        reward = rewardPool[3];
                        Debug.Log(DoorScript.selectedElements[relement]);

                        GameObject nul = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                        if (nul != null)
                        {
                            finalReward = nul;
                            finalReward.SetActive(false);
                        }
                        return;
                    case "starfall":
                        reward = rewardPool[4];
                        Debug.Log(DoorScript.selectedElements[relement]);

                        GameObject sta = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                        if (sta != null)
                        {
                            finalReward = sta;
                            finalReward.SetActive(false);
                        }
                        return;
                    case "rust":
                        reward = rewardPool[5];
                        Debug.Log(DoorScript.selectedElements[relement]);

                        GameObject rus = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                        if (rus != null)
                        {
                            finalReward = rus;
                            finalReward.SetActive(false);
                        }
                        return;
                    case "tectonic":
                        reward = rewardPool[6];
                        Debug.Log(DoorScript.selectedElements[relement]);

                        GameObject tec = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                        if (tec != null)
                        {
                            finalReward = tec;
                            finalReward.SetActive(false);
                        }
                        return;
                    case "radiation":
                        reward = rewardPool[7];
                        Debug.Log(DoorScript.selectedElements[relement]);

                        GameObject rad = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                        if (rad != null)
                        {
                            finalReward = rad;
                            finalReward.SetActive(false);
                        }
                        return;

                }
                return;
            case "boomerang":
                reward = rewardPool[8];
                

                GameObject boo = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                if (boo != null)
                {
                    finalReward = boo;
                    finalReward.SetActive(false);
                }
                return;
            case "money":
                reward = rewardPool[9];
                

                GameObject mon = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                if (mon != null)
                {
                    finalReward = mon;
                    finalReward.SetActive(false);
                }
                return;
            case "health":
                reward = rewardPool[10];
                

                GameObject ball = Instantiate(reward, rewardPosition.transform.position, rewardPosition.transform.rotation);
                if (ball != null)
                {
                    finalReward = ball;
                    finalReward.SetActive(false);
                }
                return;
        }

        


        

    }

    // Update is called once per frame
    void Update()
    {
        

        if (finished && BoonSTaticInfo.enemiesAlive.Count == 0 && waves < 4)
        {

            StartCoroutine(StartWaves());
            finished = false;
        }
        else if (waves == 4 && BoonSTaticInfo.enemiesAlive.Count == 0)

        { 
            if (!finishedFR)
            {
                if (finalReward != null)
                { finalReward.SetActive(true); }
                finishedFR = true;
                //Debug.Log("FINISHED FR");
                roomEnded = true;
            }
            
        }
    }
    IEnumerator StartWaves()
    {
        yield return new WaitForSeconds(timebeforeStart);
        StartCoroutine(SummonEnemy());
    }

    IEnumerator SummonEnemy()
    {
        yield return new WaitForSeconds(summonSpeed);
        if (!finished)
        {
            if (wave1.Count > 0 && waves == 1)
            {
                int rspawn = Random.Range(1, spawnPoints.Count);


                GameObject enemyToSpawn = wave1[0];
                GameObject ball = Instantiate(enemyToSpawn, spawnPoints[rspawn].transform.position, transform.rotation);
                wave1.Remove(enemyToSpawn);
                

            }
            else if (wave2.Count > 0 && waves == 2 && BoonSTaticInfo.UPGRADES >= 3)
            {
                int rspawn = Random.Range(1, spawnPoints.Count);


                GameObject enemyToSpawn = wave2[0];
                GameObject ball = Instantiate(enemyToSpawn, spawnPoints[rspawn].transform.position, transform.rotation);
                wave2.Remove(enemyToSpawn);
                
            }
            else if (wave3.Count > 0 && waves == 3 && BoonSTaticInfo.UPGRADES >= 6)
            {
                int rspawn = Random.Range(1, spawnPoints.Count);


                GameObject enemyToSpawn = wave3[0];
                GameObject ball = Instantiate(enemyToSpawn, spawnPoints[rspawn].transform.position, transform.rotation);
                wave3.Remove(enemyToSpawn);
                
            }
            else
            {
                //Debug.Log("END");
                finished = true;
                waves++;
            }
            StartCoroutine(SummonEnemy());
        }
        
    }


}
