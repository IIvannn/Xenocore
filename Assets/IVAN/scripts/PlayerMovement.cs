using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;


public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public static Transform playerPosition;
    public CharacterController controller;
    public Animator animator;
    public GameObject playerSprite;
    [Header("Movement")]
    public float speed = 5f;
    public float dashSpeed = 10f;
    public float dashTime = 1f;
    public float dashCooldwon = 5f;
    public int dashCharges = 2;
    float sprintSpeedBonus = 0;
    int dashesLeft;
    bool dashOnCooldwon = false;
    bool moving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dashesLeft = dashCharges;
        playerPosition = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        

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


        Vector3 move = (transform.right * input.x + transform.forward * input.y).normalized;
        controller.Move(move * (speed + sprintSpeedBonus) * Time.deltaTime);

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
            }
            else if (move.x < 0)
            {
                playerSprite.transform.localScale = new Vector3(1, 1, 1);
                
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
            

            dashesLeft--;
            //Debug.Log("dashes left: " + dashesLeft);
            float startTime = Time.time;

            while (Time.time < startTime + dashTime)
            {
                controller.Move(move * dashSpeed * Time.deltaTime);
                yield return null;
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
