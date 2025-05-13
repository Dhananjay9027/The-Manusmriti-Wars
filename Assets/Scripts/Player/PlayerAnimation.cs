using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    AimBaseState currentState;
    public ShootState Aim=new ShootState();
    public AimState Idle=new AimState();

    [SerializeField] private PlayerController player;
    [SerializeField] private GameInput gameInput;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        SwitchState(Idle);
    }
    private void Update()
    {
        animator.SetBool("IsWalking", player.Is_Walking());
        animator.SetBool("Sprint", player.Is_Running());
        animator.SetBool("Jump", player.Is_Jumping());
        currentState.UpdateState(this);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void SetAiming(bool IsAiming)
    {
        player.SetCamera(IsAiming);
    }
}
