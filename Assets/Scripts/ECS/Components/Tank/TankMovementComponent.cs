using Unity.Entities;
using Unity.Mathematics;

public struct TankMovementComponent : IComponentData
{
    public float Speed;
    public float DistanceTravelled;
}
