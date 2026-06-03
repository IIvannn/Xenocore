using System.Linq;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int numberOfRooms = 2;
    public static int[] numbers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numbers = Enumerable.Range(0, numberOfRooms).ToArray(); // 0 to 10 inclusive

        // Fisher-Yates shuffle
        for (int i = numbers.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
        }

        Debug.Log(string.Join(", ", numbers));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
