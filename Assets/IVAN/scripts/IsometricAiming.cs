using UnityEngine;
using UnityEngine.InputSystem;

namespace BarthaSzabolcs.IsometricAiming
{
    public class IsometricAiming : MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings

        [SerializeField] private LayerMask groundMask;

        #endregion
        #region Private Fields

        public Camera mainCamera;
        public static Transform cameraTransform;
        public static Vector3 mousePosition;

        #endregion

        #endregion


        #region Methods

        #region Unity Callbacks

        private void Start()
        {
            // Cache the camera, Camera.main is an expensive operation.
            mainCamera = Camera.main;
            
        }

        private void Update()
        {
            cameraTransform = Camera.main.transform;
            Aim();
        }

        #endregion

        private void Aim()
        {
            
            var (success, position) = GetMousePosition();
            if (success)
            {
                // Calculate the direction
                var direction = position - transform.position;
                mousePosition = position;
                // You might want to delete this line.
                // Ignore the height difference.
                direction.y = 0;

                // Make the transform look in the direction.
                transform.forward = direction;
            }
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var ray = mainCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            {
                return (true, hitInfo.point);
            }

            return (false, Vector3.zero);
        }

        #endregion
    }
}