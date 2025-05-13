using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private Transform EvilDeath;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            takeDamage(2);
        }
    }
    private void takeDamage(int damage)
    {
        Health-=damage;
        if(Health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Instantiate(EvilDeath, gameObject.transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
