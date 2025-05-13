using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    [SerializeField] private Transform playerDamage;
    private Rigidbody bulletRigidbody;
    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 80f;
        bulletRigidbody.velocity = transform.forward*speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>()!=null)
        {
           // Debug.Log("target");
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }
        else if (other.CompareTag("Enemy"))
        {
            Instantiate(playerDamage, transform.position, Quaternion.identity);
        }
        else
        {
           //ebug.Log("nahh");
            Instantiate(vfxHitRed,transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
