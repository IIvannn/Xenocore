using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Waver : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> wave1 = new List<GameObject>();
    public List<GameObject> wave2 = new List<GameObject>();
    public List<GameObject> wave3 = new List<GameObject>();
    public int waves = 0;
    public float timebeforeStart = 3;
    public float summonSpeed = 0.3f;
    bool finished = false;
    bool finishedFR = false;

    public GameObject reward;


    public static bool roomEnded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reward.SetActive(false);
        StartCoroutine(StartWaves());
        waves++;
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
                reward.SetActive(true);
                finishedFR = true;
                Debug.Log("FINISHED FR");
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
            else if (wave2.Count > 0 && waves == 2)
            {
                int rspawn = Random.Range(1, spawnPoints.Count);


                GameObject enemyToSpawn = wave2[0];
                GameObject ball = Instantiate(enemyToSpawn, spawnPoints[rspawn].transform.position, transform.rotation);
                wave2.Remove(enemyToSpawn);
                
            }
            else if (wave3.Count > 0 && waves == 3)
            {
                int rspawn = Random.Range(1, spawnPoints.Count);


                GameObject enemyToSpawn = wave3[0];
                GameObject ball = Instantiate(enemyToSpawn, spawnPoints[rspawn].transform.position, transform.rotation);
                wave3.Remove(enemyToSpawn);
                
            }
            else
            {
                Debug.Log("END");
                finished = true;
                waves++;
            }
            StartCoroutine(SummonEnemy());
        }
        
    }


}
