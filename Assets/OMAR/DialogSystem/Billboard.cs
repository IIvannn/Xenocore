using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        //icon look to position of the camera
        transform.LookAt(transform.position + cam.forward);
    }
}
