using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

[RequireMatchingQueriesForUpdate]
[UpdateAfter(typeof(LookAtTargetSystem))]
public partial class RocketMovementSystem : SystemBase
{
    private EntityQuery _query;

    protected override void OnCreate()
    {
        _query = new EntityQueryBuilder(Allocator.Temp)
        .WithAllRW<RocketMovementComponent>()
        .WithAllRW<LocalTransform>()
        .Build(this);
    }
    protected override void OnUpdate()
    {
        if(GameManager.Instance != null)
        {
            if(GameManager.Instance.IsGameOver)
            {
                return;
            }
        }
        
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        EntityCommandBuffer commandBuffer = ecbSingleton.CreateCommandBuffer(World.Unmanaged);
        Dependency = new RocketMovementJob
        {
            DeltaTime = SystemAPI.Time.DeltaTime,
            Ecb = commandBuffer.AsParallelWriter()
        }.ScheduleParallel(_query, Dependency);
    }
}
