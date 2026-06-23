using UnityEngine;

public class BoomerangSpecial : MonoBehaviour
{
    public float duration;
    public float damage;
    public string type;
    public float critChance;
    public float critDamage;
    public float rotationSpeed;
    public GameObject source;
    public Transform b1;
    public Transform b2;

    float bspeed = 0;
    float bsize = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        b1.Rotate(0, 0, (-(rotationSpeed + bspeed) * Time.deltaTime) * 5);
        b2.Rotate(0, 0, (-(rotationSpeed + bspeed) * Time.deltaTime) * 5);


        if (BoonSTaticInfo.boomerangSpeed)
        {
            bspeed = BoonSTaticInfo.boomerangSpecialSpeedBonus;
        }
        transform.Rotate(0, (rotationSpeed+bspeed) * Time.deltaTime, 0);
        transform.position = PlayerMovement.playerPosition.position;
        //Debug.Log(rotationSpeed + bspeed);

        transform.localScale = new Vector3((1 + (bsize * Time.deltaTime)), 1, (1 + (bsize * Time.deltaTime)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyDamage>().TakeDamage(damage, type, critChance, critDamage, source);
            other.GetComponent<EnemyDamage>().ApplyStatus(type, source);
            if (BoonSTaticInfo.boomerangSpecialGrow)
            {
                if (bsize<140)
                {
                    bsize += BoonSTaticInfo.boomerangSpecialGrowBonus;
                    Debug.Log(bsize);
                }
            }

            if (BoonSTaticInfo.boomerangSpecialCooldwon)
            {
                PlayerMovement playerMovement = source.GetComponent<PlayerMovement>();
                playerMovement.dashOnCooldwon = false;
            }
        }
    }
}
