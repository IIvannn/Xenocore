using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;


using static Unity.VisualScripting.Member;
public class TectonicZone : MonoBehaviour
{
    List<GameObject> enemiesInRange = new List<GameObject>();
    public LayerMask enemyLayer;
    float baseScale = 0f;
    float bonusscale = 0;
    bool tr = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TectonicDamage());
        StartCoroutine(TectonicDuration());
        baseScale = BoonSTaticInfo.tectonicRange;

    }
    void Update()
    {
        float finalscale = baseScale + bonusscale;
        
        bonusscale += BoonSTaticInfo.tectonicSpreadSpeed*Time.deltaTime;
        //Debug.Log(finalscale);
        transform.localScale = new Vector3(finalscale, 0.2f, finalscale);
    }
    IEnumerator TectonicDamage()
    {
        bonusscale += BoonSTaticInfo.tectonicSpread;
        
        //Debug.Log(transform.localScale.x);

        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                if (BoonSTaticInfo.tremble)
                {

                    float rchance = Random.Range(1, 100);
                    //Debug.Log(rchance);
                    if (rchance < BoonSTaticInfo.trembleChance && !enemy.GetComponent<EnemyDamage>().fissured)
                    {
                        enemy.GetComponent<EnemyDamage>().fissure++;
                    }
                }


                enemy.GetComponent<EnemyDamage>().TakeDamage(BoonSTaticInfo.tectonicDamage, "tectonic", 0, 0, null);
                if (BoonSTaticInfo.mudbath)
                {
                    float mchance = Random.Range(1, 100);
                    if (mchance < BoonSTaticInfo.mudbathChance && PlayerDamage.playerArmor < 15)
                    {
                        PlayerDamage.playerArmor += 5;
                    }
                }
                if (BoonSTaticInfo.volcanic)
                {
                    float rchance = Random.Range(1, 100);
                    //Debug.Log(rchance);
                    if (rchance < BoonSTaticInfo.volcanicChance)
                    {
                        enemy.GetComponent<EnemyDamage>().ApplyStatus("volcanic", null);
                    }
                }
            }

            
            
        }
        
        yield return new WaitForSeconds(BoonSTaticInfo.tectonicAttackSpeed);
        
        StartCoroutine(TectonicDamage());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Add(other.gameObject);
            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && BoonSTaticInfo.troglodite && tr == true)
        {
            PlayerDamage.tr = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Remove(other.gameObject);
            }
        }
        if (other.CompareTag("Player") && BoonSTaticInfo.troglodite)
        {
            PlayerDamage.tr = false;
        }
    }


    IEnumerator TectonicDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.tectonicDuration);
        tr = false;
        PlayerDamage.tr = false;
        
        Death();
    }

    public void Death()
    {
        
        BoonSTaticInfo.tectonicCurrentCount--;
        Destroy(gameObject,0.5f);

    }

}
