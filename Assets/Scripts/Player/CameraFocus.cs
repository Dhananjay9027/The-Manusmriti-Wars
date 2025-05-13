using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private Transform currentPlayer; // Reference to the current target (player or alien)
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset; // Offset between camera and target

    [SerializeField] private Vector3 ZoomOffset;
    [SerializeField] private Vector3 NormalOffset;
    [SerializeField] private float ReduceSensitivity;

    [SerializeField] private float Sensitivity;
    private float mouseSensitivity; // Sensitivity of the mouse movement
    
    private float pitch = 0f; // Vertical rotation
    private float yaw = 0f; // Horizontal rotation

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        //if (currentPlayer != null)
        //{
        //    offset = NormalOffset + currentPlayer.position;
        //    mouseSensitivity = Sensitivity;
        //}
    }

    private void LateUpdate()
    {
        HandleCamera();
    }

    private void HandleCamera()
    {
        if (currentPlayer != null)
        {
            // Handle mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity*Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity*Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -35f, 60f); // Limit vertical rotation

            // Rotate the camera around the player
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 desiredPosition = currentPlayer.position + rotation * offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;
            transform.LookAt(currentPlayer);
        }
    }
    public void SetTarget(Transform newTarget,bool IsZoom)
    {
        Debug.Log(IsZoom);
        currentPlayer = newTarget;
        if (currentPlayer != null)
        {
            offset =  IsZoom ? ZoomOffset : NormalOffset;
            mouseSensitivity = IsZoom ? ReduceSensitivity : Sensitivity;
         
        }
        
    }
}
