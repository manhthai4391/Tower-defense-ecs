using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Events;

public partial class TankCollisionSystem : SystemBase
{
    public float3 EndPoint;
    public UnityEvent OnTankReachEndPoint = new UnityEvent();
    public UnityEvent OnTankDestroy = new UnityEvent();
    private EntityQuery _rocketQuery;

    protected override void OnCreate()
    {
        _rocketQuery = new EntityQueryBuilder(Allocator.Temp)
        .WithAll<CollisionComponent>()
        .WithAll<LocalTransform>()
        .WithAll<DamageComponent>()
        .WithAll<RocketTag>()
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

        NativeArray<Entity> rocketEntities = _rocketQuery.ToEntityArray(Allocator.TempJob);
        NativeArray<LocalTransform> rocketTransformArray = _rocketQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);
        NativeArray<CollisionComponent> rocketCollisionArray = _rocketQuery.ToComponentDataArray<CollisionComponent>(Allocator.TempJob);
        NativeArray<DamageComponent> rocketDamageArray = _rocketQuery.ToComponentDataArray<DamageComponent>(Allocator.TempJob);

        Entities.ForEach((Entity tankEntity, ref TankHealthComponent tankHealth, in LocalTransform tankTransform, in CollisionComponent tankCollision) => 
        {
            if(tankHealth.Value <= 0)
                return;

            int initialHealth = tankHealth.Value;
            for(int i = 0; i < rocketTransformArray.Length; i++)
            {
                //Collision check by distance
                if(math.distance(tankTransform.Position, rocketTransformArray[i].Position) < tankCollision.Radius + rocketCollisionArray[i].Radius)
                {
                    EntityManager.DestroyEntity(rocketEntities[i]);
                    if(tankHealth.Value > 0)
                    {
                        tankHealth.Value -= rocketDamageArray[i].Value;
                        //Destroy tank entity
                        if(tankHealth.Value <= 0)
                        {
                            OnTankDestroy?.Invoke();
                            EntityManager.DestroyEntity(tankEntity);
                            return;
                        }
                    }
                }
            }

            //Check if the tank reach the end point
            if(math.distance(tankTransform.Position, EndPoint) < tankCollision.Radius)
            {
                OnTankReachEndPoint?.Invoke();
                EntityManager.DestroyEntity(tankEntity);
                return;
            }

            //if the tank took damage but still alive at this point then apply the damage
            EntityManager.SetComponentData(tankEntity, tankHealth);

        })
        .WithoutBurst()
        .WithStructuralChanges()
        .Run();

        rocketEntities.Dispose();
        rocketTransformArray.Dispose();
        rocketCollisionArray.Dispose();
        rocketDamageArray.Dispose();
    }
}
