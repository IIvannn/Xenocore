using UnityEngine;
using UnityEngine.InputSystem;

public class boomerangScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float returnSpeed = 1.0f;
    public float rotateSpeed = 10f;
    bool returning = false;
    public GameObject source;
    public AudioSource shootsnd;
    public LayerMask groundMask;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(gameObject, 8);
        //shootsnd.Play();
        //Debug.Log(type);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, 0.5f, groundMask))
        {
            returning = true;
        }


        if (Mouse.current.leftButton.wasPressedThisFrame && returning == false)
        {
            returning = true;
        }

        if (returning)
        {
            moveToPlayer();
        }
        else
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (source == null)
        {
            destroyProj();
        }
    }

    void moveToPlayer()
    {
        Vector3 dir = (source.transform.position - transform.position).normalized;
        transform.position += dir * returnSpeed * Time.deltaTime;
    }
    void RotateToPlayer()
    {
        Vector3 dir = source.transform.position - transform.position;
        dir.y = 0f;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
    }

    void destroyProj()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerShoot>().currentAmmo = other.GetComponent<PlayerShoot>().ammo;
            //Debug.Log("Player has " + other.GetComponent<PlayerShoot>().currentAmmo + " ammo");
            destroyProj();
        }
    }

}
