using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public float interactionRange = 4f;
    public GameObject indicator;
    int numberOfRooms = 12;
    public static int currentRoom = 0;
    public static int[] numbers;

    public int shopRoom = 2;
    public static bool shopBeforeBoss = false;

    
    public static string[] chosenElements;

    public List<string> possibleElements = new List<string>();
    public static List<string> selectedElements = new List<string>();

    public List<string> possibleRewards = new List<string>();
    public static List<string> selectedRewards = new List<string>();


    Scene m_Scene;
    string sceneName;
    public static Transform doorPos;
    

    void Start()
    {
        doorPos = transform;
        Debug.Log(shopRoom);

        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;

        if (sceneName == "Spawnroom")
        {
            currentRoom = 0;
        }


        if (currentRoom == 0)
        {
            numbers = Enumerable.Range(0, numberOfRooms).ToArray(); // 0 to 10 inclusive


            for (int i = numbers.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
            }

            //Debug.Log(string.Join(", ", numbers));

            List<string> availableElements = new List<string>(possibleElements);

            for (int i = 0; i < 4 && availableElements.Count > 0; i++)
            {
                int randomIndex = Random.Range(0, availableElements.Count);

                selectedElements.Add(availableElements[randomIndex]);
                availableElements.RemoveAt(randomIndex); // Prevents duplicates
            }

            Debug.Log(string.Join(", ", selectedElements));

            List<string> availableRewards = new List<string>(possibleRewards);

            for (int i = 0; i < 12 && availableRewards.Count > 0; i++)
            {
                int randomIndex = Random.Range(0, availableRewards.Count);

                selectedRewards.Add(availableRewards[randomIndex]);
                availableRewards.RemoveAt(randomIndex); // Prevents duplicates
            }

            Debug.Log(string.Join(", ", selectedRewards));
        }

        
       


        //Debug.Log(currentRoom);

    }


    void Update()
    {
        if (!PlayerDamage.dead)
        {
            float distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);
            if (distance <= interactionRange)
            {
                
                if (sceneName == "Spawnroom" || sceneName == "IvanroomShop" || sceneName == "IvanroomEnd" && BoonSTaticInfo.enemiesAlive.Count == 0)
                {
                    indicator.SetActive(true);
                    if (Keyboard.current.eKey.wasPressedThisFrame)
                        { Enter();
                        BoonSTaticInfo.radiationCurrentCount = 0;
                        BoonSTaticInfo.tectonicCurrentCount = 0;
                        BoonSTaticInfo.nullCurrentCount = 0;
                    }
                }
                else if (UpgradeManager.upgradeTaken)
                {
                    indicator.SetActive(true);
                    if (Keyboard.current.eKey.wasPressedThisFrame)
                    { Enter();
                        BoonSTaticInfo.radiationCurrentCount = 0;
                        BoonSTaticInfo.tectonicCurrentCount = 0;
                        BoonSTaticInfo.nullCurrentCount = 0;
                    }
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
        
        Waver.roomEnded = false;
        BoonSTaticInfo.reaperBonus = 0;
        PlayerDamage.currentHp += (BoonSTaticInfo.doorHeal*BoonSTaticInfo.healingMultiplier);

        if (sceneName == "After Boss Room")
        {
            BoonSTaticInfo.RESETUPGRADES();
            selectedElements.Clear();
            selectedRewards.Clear();
            SceneManager.LoadScene("Spawnroom");
            return;
        }

        if (sceneName == "IvanroomEnd")
        {
            SceneManager.LoadScene("After Boss Room");
        }
        else if (currentRoom < numberOfRooms)
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
                        //Debug.Log("room 1");
                        SceneManager.LoadScene("Room 1");
                        currentRoom++;
                        break;

                    case 1:
                        //Debug.Log("room 2");
                        SceneManager.LoadScene("Room 2");
                        currentRoom++;
                        break;

                    case 2:
                        //Debug.Log("room 3");
                        SceneManager.LoadScene("Room 3");
                        currentRoom++;
                        break;

                    case 3:
                        //Debug.Log("room 3");
                        SceneManager.LoadScene("Room 4");
                        currentRoom++;
                        break;
                    case 4:
                        //Debug.Log("room 3");
                        SceneManager.LoadScene("Room 5");
                        currentRoom++;
                        break;
                    case 5:
                        //Debug.Log("room 3");
                        SceneManager.LoadScene("Room 6");
                        currentRoom++;
                        break;
                    case 6:
                        //Debug.Log("room 3");
                        SceneManager.LoadScene("Room 7");
                        currentRoom++;
                        break;
                    case 7:
                        //Debug.Log("room 3");
                        SceneManager.LoadScene("Room 8");
                        currentRoom++;
                        break;
                    case 8:
                        //Debug.Log("room 3");
                        SceneManager.LoadScene("Room 9");
                        currentRoom++;
                        break;
                    case 9:
                        //Debug.Log("room 3");
                        SceneManager.LoadScene("Room 10");
                        currentRoom++;
                        break;
                    case 10:
                        //Debug.Log("room 3");
                        SceneManager.LoadScene("Room 11");
                        currentRoom++;
                        break;
                    case 11:
                        //Debug.Log("room 3");
                        SceneManager.LoadScene("Room 12");
                        currentRoom++;
                        break;
                    //case 12:
                    //    //Debug.Log("room 3");
                    //    SceneManager.LoadScene("Room ");
                    //    currentRoom++;
                    //    break;

                }
            }
            
        }
        else
        {

            if (!shopBeforeBoss)
            {
                Debug.Log("shop room");
                SceneManager.LoadScene("IvanroomShop");
                shopBeforeBoss = true;
            }
            

            else
            {
                Debug.Log("end room");
                SceneManager.LoadScene("IvanroomEnd");
            }

            
        }

    }
}
