using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct TankSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach(RefRW<TankSpawnerComponent> spawner in SystemAPI.Query<RefRW<TankSpawnerComponent>>())
        {
            if(!spawner.ValueRO.Initialized)
            {
                spawner.ValueRW.StartTime = (float)SystemAPI.Time.ElapsedTime;
                spawner.ValueRW.Initialized = true;
            }

            if(spawner.ValueRO.TankSpawned >= spawner.ValueRO.Amount)
            {
                state.Enabled = false;
            }

            float initTime = spawner.ValueRO.InitialDelay + spawner.ValueRO.StartTime;

            if(initTime > SystemAPI.Time.ElapsedTime)
            {
                return;
            }

            float nextTimeToSpawn = initTime + spawner.ValueRO.TankSpawned * spawner.ValueRO.CoolDown;
            if(nextTimeToSpawn > SystemAPI.Time.ElapsedTime)
            {
                return;
            }

            Entity tankEntity = state.EntityManager.Instantiate(spawner.ValueRO.TankPrefabEntity);
            state.EntityManager.SetComponentData(tankEntity, LocalTransform.FromPosition(spawner.ValueRO.SpawnPosition));

            spawner.ValueRW.TankSpawned++;
        }
    }    
}
