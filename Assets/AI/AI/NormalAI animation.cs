using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAIanimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Rigidbody rigid;
    private float animSpeed=1f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
    }

}
