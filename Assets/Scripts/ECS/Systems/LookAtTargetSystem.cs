using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;

[RequireMatchingQueriesForUpdate]
[UpdateAfter(typeof(TowerTargetDetectionSystem))]
[UpdateBefore(typeof(RocketMovementSystem))]
public partial class LookAtTargetSystem : SystemBase
{
    private EntityQuery _query;

    protected override void OnCreate()
    {
        _query = new EntityQueryBuilder(Allocator.Temp)
        .WithAllRW<LookAtTargetComponent>()
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

        Dependency = new LookAtTargetJob
        {
            DeltaTime = SystemAPI.Time.DeltaTime
        }.ScheduleParallel(_query, Dependency);
    }
}
