using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;

[UpdateBefore(typeof(LookAtTargetSystem))]
public partial class TowerTargetDetectionSystem : SystemBase
{
    private EntityQuery _tankQuery;

    protected override void OnCreate()
    {
        _tankQuery = new EntityQueryBuilder(Allocator.Temp)
        .WithAll<TankHealthComponent>()
        .WithAll<LocalTransform>()
        .WithAll<TankMovementComponent>()
        .WithAll<CollisionComponent>()
        .WithAll<TankTag>()
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

        NativeArray<CollisionComponent> tankCollisionList = _tankQuery.ToComponentDataArray<CollisionComponent>(Allocator.TempJob);
        NativeArray<LocalTransform> tankTransformList = _tankQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);
        NativeArray<TankMovementComponent> tankMovementList = _tankQuery.ToComponentDataArray<TankMovementComponent>(Allocator.TempJob);
        NativeArray<Entity> tankEntityList = _tankQuery.ToEntityArray(Allocator.TempJob);

        Dependency = Entities.ForEach((ref TowerDetectionComponent towerDetection, ref LocalTransform towerTransform, ref LookAtTargetComponent lookAt, in CollisionComponent collision) =>
        {
            int target = -1;
            float longestDistance = float.MinValue;
            for(int i = 0; i < tankTransformList.Length; i++)
            {
                if(tankEntityList[i] == Entity.Null)
                    continue;

                //Collision check by distance
                if(math.distance(towerTransform.Position, tankTransformList[i].Position) < collision.Radius + tankCollisionList[i].Radius)
                {
                    if(target == -1)
                    {
                        target = i;
                        longestDistance = tankMovementList[i].DistanceTravelled;
                    }
                    else
                    {
                        //select target based on distance travelled
                        if(tankMovementList[i].DistanceTravelled > longestDistance)
                        {
                            target = i;
                            longestDistance = tankMovementList[i].DistanceTravelled;
                        }
                    }
                }
            }

            if(target == -1)
            {
                towerDetection.IsTargetInRange = false;
                lookAt.ShouldLook = false;
            }
            else
            {
                towerDetection.IsTargetInRange = true;
                towerDetection.Target = tankEntityList[target];
                lookAt.ShouldLook = true;
                lookAt.TargetPosition = tankTransformList[target].Position;
            }
        })
        .WithBurst()
        .Schedule(Dependency);

        Dependency = tankCollisionList.Dispose(Dependency);
        Dependency = tankMovementList.Dispose(Dependency);
        Dependency = tankTransformList.Dispose(Dependency);
        Dependency = tankEntityList.Dispose(Dependency);
    }
}
