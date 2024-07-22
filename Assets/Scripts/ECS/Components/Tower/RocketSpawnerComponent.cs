using Unity.Entities;

public struct RocketSpawnerComponent : IComponentData
{
    public float Cooldown;
    public Entity RocketEntityPrefab;
    public float LastSpawnTime;
    public Entity SpawnedRocketEntity;
    public bool FirstRocketHasLaunched;
}
