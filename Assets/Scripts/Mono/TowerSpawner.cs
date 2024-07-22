using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class TowerSpawner : MonoBehaviour
{
    public void SpawnTowerEntity(Vector3 position)
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entity towerSpawnerEntity = entityManager.CreateEntityQuery(typeof(TowerSpawnerComponent)).GetSingletonEntity();
        TowerSpawnerComponent spawner = entityManager.GetComponentData<TowerSpawnerComponent>(towerSpawnerEntity);

        #region Spawn Entity
        Entity towerBaseEntity = entityManager.Instantiate(spawner.TowerBasePrefabEntity);

        Entity towerEntity = entityManager.Instantiate(spawner.TowerPrefabEntity);
        
        Entity rocketEntity = entityManager.Instantiate(spawner.RocketPrefabEntity);
        #endregion

        #region Set Up Component
        LocalTransform towerBaseTransform = entityManager.GetComponentData<LocalTransform>(towerBaseEntity);
        towerBaseTransform.Position = position;
        entityManager.SetComponentData(towerBaseEntity, towerBaseTransform);

        LocalTransform towerTransform = entityManager.GetComponentData<LocalTransform>(towerEntity);
        towerTransform.Position = position;
        towerTransform.Rotation = quaternion.Euler(-90, 0, 0);
        entityManager.SetComponentData(towerEntity, towerTransform);

        LocalTransform rocketTransform = entityManager.GetComponentData<LocalTransform>(rocketEntity);
        rocketTransform.Position = position;
        rocketTransform.Rotation = quaternion.Euler(-90, 0, 0);
        entityManager.SetComponentData(rocketEntity, rocketTransform);  

        RocketSpawnerComponent rocketSpawner = entityManager.GetComponentData<RocketSpawnerComponent>(towerEntity);
        rocketSpawner.SpawnedRocketEntity = rocketEntity;
        rocketSpawner.RocketEntityPrefab = spawner.RocketPrefabEntity;
        entityManager.SetComponentData(towerEntity, rocketSpawner);

        RocketMovementComponent rocketMovement = entityManager.GetComponentData<RocketMovementComponent>(rocketEntity);
        rocketMovement.SpawnPosition = position; 
        entityManager.SetComponentData(rocketEntity, rocketMovement);

        RocketTargetDetectionComponent rocketTargetDetection = entityManager.GetComponentData<RocketTargetDetectionComponent>(rocketEntity);
        rocketTargetDetection.SpawnTower = towerEntity;
        entityManager.SetComponentData(rocketEntity, rocketTargetDetection);
        #endregion
    }
}
