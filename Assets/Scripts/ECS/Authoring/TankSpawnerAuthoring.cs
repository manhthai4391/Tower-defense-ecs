using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class TankSpawnerAuthoring : MonoBehaviour
{
    public Transform SpawnPoint;
    public int Amount;
    public GameObject TankPrefab;
    public float CoolDown;
    public float InitialDelay;
}

public class TankSpawnerBaker : Baker<TankSpawnerAuthoring>
{
    public override void Bake(TankSpawnerAuthoring authoring)
    {
        Entity tankSpawnerEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(tankSpawnerEntity, new TankSpawnerComponent 
        {
            SpawnPosition = new float3 (authoring.SpawnPoint.position),
            Amount = authoring.Amount,
            TankPrefabEntity = GetEntity(authoring.TankPrefab, TransformUsageFlags.Dynamic),
            CoolDown = authoring.CoolDown,
            InitialDelay = authoring.InitialDelay,
            Initialized = false,
        });
    }
}
