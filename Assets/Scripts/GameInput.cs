using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerAction playerinputaction;
    private void Awake()
    {
        playerinputaction = new PlayerAction();
        playerinputaction.Player.Enable();
    }
    public Vector2 GetMovementNormalized()
    {
        Vector2 inputVector = playerinputaction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized; //use to noramlize the condition in which the vector become (1,1) and the charactere move at high magnitude ay diagopnal
        return inputVector;
    }

    public bool IsRunning()
    {
        return playerinputaction.Player.Sprint.ReadValue<float>() > 0;
    }

    public bool IsJumping()
    {
        return playerinputaction.Player.Jump.ReadValue<float>() > 0;
    }
}
