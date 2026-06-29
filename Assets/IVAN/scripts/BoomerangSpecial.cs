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
        b1.Rotate(0, 0, (-(rotationSpeed + bspeed) * Time.deltaTime) * -5);
        b2.Rotate(0, 0, (-(rotationSpeed + bspeed) * Time.deltaTime) * -5);


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
        float bshen = 1;
        if (BoonSTaticInfo.shenanigans)
        {
            int shen = Random.Range(1, 4);
            
            //Debug.Log("shenanigans chance: " + shen);
            if (shen == 1)
            {
                bshen = 0.8f;
            }
            else if (shen == 2)
            {
                bshen = 1.05f;
            }
            else
            {
                bshen = 1.3f;
            }
        }
        

        if (other.gameObject.CompareTag("Prism"))
        {
            other.GetComponent<Prism>().Hurt();
            Debug.Log("prism");
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyDamage>().TakeDamage(damage* bshen, type, critChance, critDamage, source);
            other.GetComponent<EnemyDamage>().ApplyStatus(type, source);
            source.GetComponent<PlayerShoot>().onHit();
            if (BoonSTaticInfo.boomerangSpecialGrow)
            {
                if (bsize<155)
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
