using Unity.Entities;
using Unity.Mathematics;

public struct TankSpawnerComponent : IComponentData
{
     public float3 SpawnPosition;
     public int Amount;
     public Entity TankPrefabEntity;
     public float CoolDown;
     public float InitialDelay;

     //SPAWN SYSTEM CACHES
     public bool Initialized;
     public float StartTime;
     public int TankSpawned;

}
