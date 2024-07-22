using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct RocketMovementJob : IJobEntity
{
    public float DeltaTime;
    public EntityCommandBuffer.ParallelWriter Ecb;
    [BurstCompile]
    public void Execute(Entity entity, [ChunkIndexInQuery] int chunkIndex, ref RocketMovementComponent movement, ref LocalTransform transform)
    {
        if(!movement.ShouldMove)
            return;
        
        if(math.distancesq(transform.Position, movement.SpawnPosition) > movement.MaxRange * movement.MaxRange)
        {
            Ecb.DestroyEntity(chunkIndex, entity);
            return;
        }

        transform.Position += movement.Speed * DeltaTime * transform.Forward();
    }
}
