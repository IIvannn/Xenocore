using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditsSimple : MonoBehaviour
{
    [Header("UI")]
    public RectTransform creditsText;     //textreference
    public float scrollSpeed = 50f;       //speed of the scroll
    public float endYPosition = 1200f;    //position at the end

    [Header("Fade")]
    public CanvasGroup fadeGroup;         //fade black
    public float fadeDuration = 2f;

    [Header("Scene")]
    public string nextScene = "MainMenu"; //select the scene

    private bool finished = false;

    void Start()
    {
        if (fadeGroup != null)
            fadeGroup.alpha = 0f;
    }

    void Update()
    {
        if (finished || creditsText == null) return;

        //scroll up
        creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        //fade and change the scene
        if (creditsText.anchoredPosition.y >= endYPosition)
        {
            finished = true;
            StartCoroutine(FadeAndLoad());
        }
    }

    IEnumerator FadeAndLoad()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            if (fadeGroup != null)
                fadeGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);

            yield return null;
        }

        SceneManager.LoadScene(nextScene);
    }
}