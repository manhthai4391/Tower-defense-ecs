using Unity.Entities;

public struct TowerSpawnerComponent : IComponentData
{
    public Entity TowerPrefabEntity;
    public Entity RocketPrefabEntity;
    public Entity TowerBasePrefabEntity;
}
