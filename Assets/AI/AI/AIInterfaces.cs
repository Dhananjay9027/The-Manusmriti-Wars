using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInterfaces : MonoBehaviour
{
    public interface IMovable
    {
        void Patrol();
        void Chase(Transform target);
    }

    public interface IAttackable
    {
        void Attack(Transform target);
    }
}
