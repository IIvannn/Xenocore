using UnityEngine;
using UnityEngine.SceneManagement;

public class musicManager : MonoBehaviour
{
    Scene m_Scene;
    string sceneName;

    public AudioSource auso;
    public AudioClip mainMusic;
    public AudioClip ambiantMusic;
    public AudioClip combatMusic;
    public AudioClip bossMusic;
    public AudioClip shopMusic;
    //AudioClip clip;

    public static string musicType = "main";



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;

        if (sceneName == "MainMenu")
        {
            auso.clip = mainMusic;
        }


        auso.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(musicType);
        switch (musicType)
        {
            case "ambiant":
                if (auso.clip != ambiantMusic)
                {
                    auso.clip = ambiantMusic;
                    auso.Play();
                }
                break;

            case "combat":
                if (auso.clip != combatMusic)
                {
                    auso.clip = combatMusic;
                    auso.Play();
                }
                break;

            case "boss":
                if (auso.clip != bossMusic)
                {
                    auso.clip = bossMusic;
                    auso.Play();
                }
                break;

            case "shop":
                if (auso.clip != bossMusic)
                {
                    auso.clip = bossMusic;
                    auso.Play();
                }
                break;
        }

    }





}
