using BarthaSzabolcs.IsometricAiming;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DamageNumber : MonoBehaviour
{
    public float speed = 5f;
    public float damage;
    public TextMeshProUGUI textDmg;
    public string type;
    float xoffset = 0.9f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.7f);
        textDmg.text = damage.ToString();
        float offset = Random.Range(-xoffset, xoffset);
        transform.position += new Vector3(offset, 0, 0);
        float bsize = damage / 50f;
        textDmg.fontSize += bsize;
        


        switch (type)
        {
            case "normal":
                textDmg.color = new Color(1f, 1f, 1f);
                break;
            case "swarm":
                textDmg.color = new Color(0.7f,1f,0.2f);
                break;
            case "haunted":
                textDmg.color = new Color(0.2f, 0.7f, 0.8f);
                break;
            case "crystallize":
                textDmg.color = new Color(0.7f, 0.7f, 0.9f);
                break;
            case "null":
                textDmg.color = new Color(0.1f, 0.0f, 0.4f);
                break;
            case "starfall":
                textDmg.color = new Color(1f, 0.1f, 0.7f);
                break;
            case "rust":
                textDmg.color = new Color(0.6f, 0.1f, 0.2f);
                break;
            case "tectonic":
                textDmg.color = new Color(0.8f, 0.6f, 0.2f);
                break;
            case "radiation":
                textDmg.color = new Color(1f, 1f, 0.2f);
                break;
            case "volcanic":
                textDmg.color = new Color(1f, 1f, 0.5f);
                break;
            case "star":
                textDmg.color = new Color(1f, 0.4f, 1f);
                break;
            case "gem":
                textDmg.color = new Color(0.7f, 0.7f, 0.9f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        textDmg.alpha -= 0.6f*Time.deltaTime;
        textDmg.fontSize -= 0.6f * Time.deltaTime;
        transform.rotation = IsometricAiming.cameraTransform.rotation;
        transform.position += new Vector3(0, speed*Time.deltaTime, 0);
    }
}
