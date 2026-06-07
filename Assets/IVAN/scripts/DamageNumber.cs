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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.7f);
        textDmg.text = damage.ToString();
        float offset = Random.Range(-0.3f, 0.3f);
        transform.position += new Vector3(offset, 0, 0);
        float bsize = damage / 70f;
        textDmg.fontSize += bsize;
        

        switch (type)
        {
            case "normal":
                textDmg.outlineColor = Color.black;
                break;
            case "swarm":
                textDmg.color = new Color(0.7f,1f,0.2f);
                break;
            case "haunted":
                break;
            case "crystallize":
                break;
            case "null":
                break;
            case "starfall":
                textDmg.color = new Color(1f, 0.1f, 0.7f);
                break;
            case "rust":
                break;
            case "tectonic":
                break;
            case "radiation":
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
