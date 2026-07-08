using UnityEngine;
using UnityEngine.InputSystem;

public class tips : MonoBehaviour
{
    public GameObject closeIndicator;
    public GameObject tipsmenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tipsmenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);
        if (distance < 3)
        {
            closeIndicator.SetActive(true);
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                tipsmenu.SetActive(true);
            }
        }
        else
        {
            closeIndicator.SetActive(false);
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            tipsmenu.SetActive(false);
        }

    }
}
