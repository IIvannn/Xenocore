using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public static Transform playerPosition;
    public CharacterController controller;
    public Animator animator;
    public GameObject playerSprite;
    public GameObject pob;
    public GameObject phaseDash;
    public GameObject volcanicFissure;
    public GameObject nml;
    [Header("Movement")]
    public float speed = 5f;
    public float dashSpeed = 10f;
    public float dashTime = 1f;
    public float dashCooldwon = 5f;
    public int dashCharges = 2;
    float sprintSpeedBonus = 0;
    int dashesLeft;
    public bool dashOnCooldwon = false;
    bool moving = false;
    public bool right = true;

    float btime = 0;
    float energizedBoost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dashesLeft = dashCharges;
        playerPosition = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDamage pd = GetComponent<PlayerDamage>();
        
        if (BoonSTaticInfo.noMansLand)
        {
            nml.SetActive(true);
            nml.GetComponent<AOEDamageOverTime>().damage = BoonSTaticInfo.noMansLandDamage;
            nml.GetComponent<AOEDamageOverTime>().attackSpeed = BoonSTaticInfo.noMansLandAttackSpeed;
        }


        if (BoonSTaticInfo.energized)
        {
            energizedBoost = (pd.currentEnergy / pd.energy)*BoonSTaticInfo.energizedBonus;
        }

        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            input.y += 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            input.y -= 1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            input.x += 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            input.x -= 1;
        }

        
        float finalSpeed = speed+energizedBoost;
        if (BoonSTaticInfo.massAccumulation)
        {
            finalSpeed += (BoonSTaticInfo.UPGRADES * BoonSTaticInfo.massAccumulationBonus / 2);
        }


        Vector3 move = (transform.right * input.x + transform.forward * input.y).normalized;
        controller.Move(move * (finalSpeed + sprintSpeedBonus) * Time.deltaTime);
        

        if (move == Vector3.zero)
        {
            moving = false;
        }
        else
        {
            moving = true;
            if (move.x > 0)
            {
                playerSprite.transform.localScale = new Vector3(-1, 1, 1);
                right = false;
            }
            else if (move.x < 0)
            {
                playerSprite.transform.localScale = new Vector3(1, 1, 1);
                right = true;
            }
            if (move.z <0)
            {
                animator.SetBool("down", true);
                animator.SetBool("up", false);
            }
            else if (move.z >0)
            {
                animator.SetBool("up", true);
                animator.SetBool("down", false);
            }
            else
            {
                animator.SetBool("up", false);
                animator.SetBool("down", false);
            }
        }
        

        if (animator != null)
        {
            animator.SetBool("moving", moving);
        }


        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (dashesLeft > 0)
            {
                StartCoroutine(Dash());
            }
            if (!dashOnCooldwon)
            {
                StartCoroutine(DashCooldown());
            }
            
        }

        IEnumerator Dash()
        {
            
            if (BoonSTaticInfo.phaseDash)
            {
                phaseDash.SetActive(true);
                phaseDash.GetComponent<Shockwave>().damage = BoonSTaticInfo.phaseDashDashDamage;
                btime = BoonSTaticInfo.phaseDashDashDurationBonus;
            }
            if (BoonSTaticInfo.pob)
            {
                GameObject ball = Instantiate(pob, transform.position, transform.rotation);
            }

            if (BoonSTaticInfo.eruption)
            {
                GameObject ball = Instantiate(volcanicFissure, transform.position, transform.rotation);
                ball.GetComponent<Shockwave>().damage = BoonSTaticInfo.eruptionDamage;
                ball.GetComponent<Shockwave>().range = BoonSTaticInfo.eruptionRange;
            }

            dashesLeft--;
            //Debug.Log("dashes left: " + dashesLeft);
            float startTime = Time.time;

            while (Time.time < startTime + (dashTime+btime))
            {
                controller.Move(move * dashSpeed * Time.deltaTime);
                yield return null;
                
            }

            if (BoonSTaticInfo.eruption)
            {
                GameObject ball = Instantiate(volcanicFissure, transform.position, transform.rotation);
                ball.GetComponent<Shockwave>().damage = BoonSTaticInfo.eruptionDamage;
                ball.GetComponent<Shockwave>().range = BoonSTaticInfo.eruptionRange;
            }

            if (BoonSTaticInfo.phaseDash)
            {
                phaseDash.SetActive(false);
                phaseDash.GetComponent<Shockwave>().enemieshit.Clear();
            }

            if (BoonSTaticInfo.boomerangSpecialCooldwon)
            {
                PlayerShoot playerShoot = GetComponent<PlayerShoot>();
                if (!playerShoot.specialing)
                {
                    playerShoot.canspecial = true;
                }
            }
        }

        IEnumerator DashCooldown()
        {
            dashOnCooldwon = true;
            //Debug.Log("dash cooldwon started");
            yield return new WaitForSeconds(dashCooldwon);
            dashOnCooldwon = false;
            //Debug.Log("dash cooldwon finished");
            dashesLeft = dashCharges;
        }
    }
}
