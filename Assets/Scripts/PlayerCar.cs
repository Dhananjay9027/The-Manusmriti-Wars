using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
   // [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameManager GameManager;
    public Transform CameraTarget;
    public bool TriggerEnter;
    public bool TriggerExit;
    private void OnTriggerEnter(Collider other)
    {
       GameManager.OnTriggerEnterCheck(other);
       
    }
    private void OnTriggerExit(Collider other)
    {
        GameManager.OnTriggerExitCheck(other);
    }

}
