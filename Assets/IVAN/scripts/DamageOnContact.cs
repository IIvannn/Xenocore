
using System.Collections;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public float damage = 1f;
    public string type = "normal";
    public GameObject source;
    bool canDie = false;

    private void Start()
    {
        StartCoroutine(CanDie());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other != source && canDie)
            {
                other.GetComponent<EnemyDamage>().TakeDamage(damage, type, 0, 0, null);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator CanDie()
    {
        yield return new WaitForSeconds(0.3f);
        canDie = true;
    }
}
