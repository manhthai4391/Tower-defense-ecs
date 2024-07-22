using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Unity.Entities;

public class TankPath : MonoBehaviour
{
    [SerializeField]
    private PathCreator path;

    [SerializeField]
    private Transform endPoint;

    private void OnEnable() 
    {
        TankMovementSystem movementSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<TankMovementSystem>();
        movementSystem.Path = path;

        TankCollisionSystem tankCollision = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<TankCollisionSystem>();
        tankCollision.EndPoint = endPoint.position;
    }
}
