using UnityEngine;
using System.Collections;

public class Prism : MonoBehaviour
{
    public GameObject shockwave;
    bool candmg = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        Destroy(gameObject,BoonSTaticInfo.prismDuration);
    }

    public void Hurt()
    {
        if (candmg)
        {
            StartCoroutine(cooldown());
            GameObject ball = Instantiate(shockwave, transform.position, transform.rotation);
            ball.GetComponent<Shockwave>().damage = BoonSTaticInfo.prismDamage;
            ball.GetComponent<Shockwave>().range = BoonSTaticInfo.prismRange;
            ball.GetComponent<Shockwave>().type = "crystallize";
        }
    }

    IEnumerator cooldown()
    {
        candmg = false;
        yield return new WaitForSeconds(BoonSTaticInfo.prismCooldown);
        candmg = true;
    }

}
