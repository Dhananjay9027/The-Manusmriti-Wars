using UnityEngine;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player; // Reference to the player object
   // [SerializeField] private Transform playerOutsideOffset; // Offset position outside the car

    private CarController currentCarController; // Reference to the current car's controller
    private bool isNearCar = false; // To check if the player is near a car
    private bool isDriving = false; // To check if the player is driving
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    void Update()
    {
        if (isNearCar && Input.GetKeyDown(KeyCode.F)) // Press Enter
        {
            Debug.Log("pressed");
            if (!isDriving)
            {
                EnterCar();
            }
            else
            {
                ExitCar();
            }
        }
    }

    void EnterCar()
    {
        if (currentCarController == null) return;

        // Disable player control and position inside the car
        VirtualCamera.Follow = currentCarController.target;
        player.SetActive(false);
        player.transform.position = currentCarController.carSeat.position;

        // Enable car driving
        isDriving = true;
        currentCarController.IsDriving = true;
    }

    void ExitCar()
    {
        if (currentCarController == null) return;

        // Enable player control and position outside the car
        player.SetActive(true);
        player.transform.position = currentCarController.playerOutsideOffset.position;
        VirtualCamera.Follow = player.GetComponent<PlayerCar>().CameraTarget;
        // Disable car driving
        isDriving = false;
        currentCarController.IsDriving = false;

        currentCarController = null; // Clear current car reference
    }

    public void OnTriggerEnterCheck(Collider other)
    {
       
        if (other.CompareTag("Car")) // Ensure the car has the "Car" tag
        {
            Debug.Log("carr");
            currentCarController = other.GetComponentInParent<CarController>();
            if (currentCarController != null)
            {
                Debug.Log("has CC");
                isNearCar = true;
            }
        }
    }

    public void OnTriggerExitCheck(Collider other)
    {
        if (other.CompareTag("Car") && other.GetComponentInParent<CarController>() == currentCarController)
        {
            isNearCar = false;
            currentCarController = null; // Clear the reference when the player leaves the car area
        }
    }
}
