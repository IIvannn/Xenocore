using UnityEngine;
using UnityEngine.InputSystem;

public class tips : MonoBehaviour
{
    public GameObject tipsmenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tipsmenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void closetips()
    {
        if (tipsmenu.activeSelf)
        {
            tipsmenu.SetActive(false);

        }
        else
        {
            tipsmenu.SetActive(true);
        }
    }
}
