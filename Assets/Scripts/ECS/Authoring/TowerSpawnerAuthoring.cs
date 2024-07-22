using Unity.Entities;
using UnityEngine;

public class TowerSpawnerAuthoring : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject TowerBasePrefab;
    public GameObject RocketPrefab;
}

public class TowerSpawnerBaker : Baker<TowerSpawnerAuthoring>
{
    public override void Bake(TowerSpawnerAuthoring authoring)
    {
        Entity towerSpawnerEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(towerSpawnerEntity, new TowerSpawnerComponent 
        {
            TowerPrefabEntity = GetEntity(authoring.TowerPrefab, TransformUsageFlags.Dynamic),
            TowerBasePrefabEntity = GetEntity(authoring.TowerBasePrefab, TransformUsageFlags.Dynamic),
            RocketPrefabEntity = GetEntity(authoring.RocketPrefab, TransformUsageFlags.Dynamic)
        });
    }
}
