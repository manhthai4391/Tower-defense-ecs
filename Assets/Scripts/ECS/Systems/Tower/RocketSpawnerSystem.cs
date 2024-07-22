using Unity.Entities;
using Unity.Transforms;

public partial class RocketSpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        if(GameManager.Instance != null)
        {
            if(GameManager.Instance.IsGameOver)
            {
                return;
            }
        }

        float now = (float)SystemAPI.Time.ElapsedTime;

        Entities.ForEach((Entity entity, ref RocketSpawnerComponent rocketSpawner, in LocalTransform towerTransform, in TowerDetectionComponent towerDetection) => 
        {
            if(rocketSpawner.FirstRocketHasLaunched == false)
            {
                if(towerDetection.IsTargetInRange)
                {
                    rocketSpawner.LastSpawnTime = now;
                    rocketSpawner.FirstRocketHasLaunched = true;
                }
                return;
            }

            if(now - rocketSpawner.LastSpawnTime > rocketSpawner.Cooldown)
            {
                //if there's no target in range and there's a rocket at the tower then return
                if(towerDetection.IsTargetInRange == false && rocketSpawner.SpawnedRocketEntity != Entity.Null)
                {
                    return;
                }

                Entity rocketEntity = EntityManager.Instantiate(rocketSpawner.RocketEntityPrefab);

                LocalTransform rocketTransform = EntityManager.GetComponentData<LocalTransform>(rocketEntity);
                rocketTransform.Position = towerTransform.Position;
                rocketTransform.Rotation = towerTransform.Rotation;

                EntityManager.SetComponentData(rocketEntity, rocketTransform);

                EntityManager.SetComponentData(rocketEntity, new RocketTargetDetectionComponent
                {
                    SpawnTower = entity
                });

                RocketMovementComponent rocketMovement = EntityManager.GetComponentData<RocketMovementComponent>(rocketEntity);
                rocketMovement.SpawnPosition = towerTransform.Position;
                rocketMovement.ShouldMove = false;
                EntityManager.SetComponentData(rocketEntity, rocketMovement);
                
                rocketSpawner.LastSpawnTime = now;
                rocketSpawner.SpawnedRocketEntity = rocketEntity;
            }
        })
        .WithStructuralChanges()
        .Run();
    }
}
