using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Healthslider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(2);
        }
    }
    public void SetMAxHealth(int health)
    {
        Healthslider.maxValue = health;
        Healthslider.value = health;
    }
    public void SetHealth(int health)
    {
        Healthslider.value = health;
    }
    public void TakeDamage(int damage)
    {
        Healthslider.value -= damage;
        if(Healthslider.value <= 0)
        {
            //''''Game Over''''
            Debug.Log("Deadd Bhaiiii");
          //  Destroy(gameObject);
        }
    }

}
