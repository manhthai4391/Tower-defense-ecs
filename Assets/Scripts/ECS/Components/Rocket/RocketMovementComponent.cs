using Unity.Entities;
using Unity.Mathematics;

public struct RocketMovementComponent : IComponentData
{
    public float Speed;
    public float MaxRange;
    public float3 SpawnPosition;
    public bool ShouldMove;
}
