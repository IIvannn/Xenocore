using UnityEngine;
using UnityEngine.InputSystem;

public class IngameUi : MonoBehaviour
{
    public GameObject pause;
    public GameObject set;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!pause.activeSelf)
            {
                pause.SetActive(true);
                pause.GetComponent<PauseMenuController>().PauseGame();
            }
        }
    }

}
