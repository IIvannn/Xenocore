using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;


using static Unity.VisualScripting.Member;
public class TectonicZone : MonoBehaviour
{
    public LayerMask enemyLayer;
    float baseScale = 0f;
    float bonusscale = 0;
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
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, transform.localScale.x, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyDamage>().TakeDamage(BoonSTaticInfo.tectonicDamage, "tectonic", 0, 0, null);

            float rchance = Random.Range(1, 100);
            //Debug.Log(rchance);
            if (rchance<35)
            {
                enemy.GetComponent<EnemyDamage>().ApplyStatus("volcanic", null);
            }
            
        }
        
        yield return new WaitForSeconds(BoonSTaticInfo.tectonicAttackSpeed);
        
        StartCoroutine(TectonicDamage());

    }


    IEnumerator TectonicDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.tectonicDuration);
        Death();
    }

    public void Death()
    {
        BoonSTaticInfo.tectonicCurrentCount--;
        Destroy(gameObject,0.5f);

    }

}
