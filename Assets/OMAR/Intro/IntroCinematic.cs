using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class IntroCinematic : MonoBehaviour
{
    public IntroSequence sequence;

    [Header("UI")]
    public Image cinematicImage;
    public TMP_Text cinematicText;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Scene")]
    public string nextSceneName = "Level1";

    private bool skipping = false;
    private Sprite lastImage = null;

    void Start()
    {
        //black background
        cinematicImage.color = new Color(1, 1, 1, 0);
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;

        StartCoroutine(PlayIntro());
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            skipping = true;
    }

    IEnumerator PlayIntro()
    {
        foreach (var line in sequence.lines)
        {
            if (skipping) break;

            //image
            if (line.image != null)
            {
                bool imageChanged = (line.image != lastImage);

                if (imageChanged)
                {
                    //fade out of the last image
                    if (lastImage != null)
                        yield return StartCoroutine(FadeImage(cinematicImage, 1f, 0f, line.imageFadeOutTime));

                    //change image
                    cinematicImage.sprite = line.image;

                    //fade in
                    yield return StartCoroutine(FadeImage(cinematicImage, 0f, 1f, line.imageFadeInTime));
                }
                else
                {
                    //same image dont change color 1,1,1,1
                    cinematicImage.sprite = line.image;
                    cinematicImage.color = new Color(1, 1, 1, 1);
                }

                lastImage = line.image;
            }

            //text typing
            cinematicText.text = "";

            if (line.font != null)
                cinematicText.font = line.font;

            //color of each lane
            cinematicText.color = line.textColor;

            //typing effect
            yield return StartCoroutine(TypeText(line.text, 0.03f));

            //sound
            if (line.sound != null)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(line.sound);
            }

            //time of the text on screen
            float txtTimer = 0f;
            while (txtTimer < line.textDisplayTime)
            {
                if (skipping) break;
                txtTimer += Time.deltaTime;
                yield return null;
            }

            cinematicText.text = "";
        }

        //load scene at ending
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator TypeText(string text, float speed)
    {
        cinematicText.text = "";
        foreach (char c in text)
        {
            if (skipping)
            {
                cinematicText.text = text;
                yield break;
            }

            cinematicText.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    IEnumerator FadeImage(Image img, float start, float end, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            if (skipping) break;

            t += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, t / duration);
            Color c = img.color;
            c.a = alpha;
            img.color = c;
            yield return null;
        }
    }
}

