using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
public partial struct LookAtTargetJob : IJobEntity
{
    public float DeltaTime;

    [BurstCompile]
    public void Execute(ref LookAtTargetComponent lookAt, ref LocalTransform transform)
    {
        if(lookAt.ShouldLook)
        {
            float3 direction = lookAt.TargetPosition - transform.Position;
            float3 directionNormalized = math.normalize(direction);

            quaternion targetRotation = quaternion.LookRotationSafe(directionNormalized, -math.forward());
            transform.Rotation = math.slerp(transform.Rotation, targetRotation, DeltaTime * lookAt.RotateSpeed);
        }
    }
}
