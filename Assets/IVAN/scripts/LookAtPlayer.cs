using BarthaSzabolcs.IsometricAiming;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LookAtPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDamage.dead)
        {
            return;
        }
        if (IsometricAiming.cameraTransform != null)
        {
            transform.rotation = IsometricAiming.cameraTransform.rotation;
        }
    }
}
