using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public float interactionRange = 4f;
    public GameObject indicator;
    int numberOfRooms = 4;
    public static int currentRoom = 0;
    public static int[] numbers;
    public int shopRoom = 2;


    Scene m_Scene;
    string sceneName;


    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        if (currentRoom == 0)
        {
            numbers = Enumerable.Range(0, numberOfRooms).ToArray(); // 0 to 10 inclusive


            for (int i = numbers.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
            }

            Debug.Log(numbers[0]);
        }
        
    }


    void Update()
    {
        if (!PlayerDamage.dead)
        {
            float distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);
            if (distance <= interactionRange)
            {
                indicator.SetActive(true);
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    Enter();
                }
            }
            else
            {
                indicator.SetActive(false);
            }
        }
        
    }

    void Enter()
    {
        if (currentRoom < numberOfRooms)
        {
            if (currentRoom == shopRoom && sceneName != "IvanroomShop")
            {
                Debug.Log("shop room");
                SceneManager.LoadScene("IvanroomShop");
            }
            else
            {
                BoonSTaticInfo.enemiesInRange.Clear();
                switch (numbers[(numbers.Length - currentRoom) - 1])
                {
                    case 0:
                        Debug.Log("room 1");
                        SceneManager.LoadScene("Ivanroom");
                        currentRoom++;
                        break;

                    case 1:
                        Debug.Log("room 2");
                        SceneManager.LoadScene("Ivanroom 1");
                        currentRoom++;
                        break;

                    case 2:
                        Debug.Log("room 3");
                        SceneManager.LoadScene("Ivanroom 2");
                        currentRoom++;
                        break;

                    case 3:
                        Debug.Log("room 3");
                        SceneManager.LoadScene("Ivanroom 3");
                        currentRoom++;
                        break;

                }
            }
            
        }
        else
        {
            Debug.Log("end room");
            SceneManager.LoadScene("IvanRoomEnd");
        }

    }
}
