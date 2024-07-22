using Unity.Entities;
using Unity.Mathematics;

public struct LookAtTargetComponent : IComponentData
{
    public float RotateSpeed;
    public float3 TargetPosition;
    public bool ShouldLook;
}
