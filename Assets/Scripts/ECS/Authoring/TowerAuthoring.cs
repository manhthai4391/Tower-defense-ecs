using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class TowerAuthoring : MonoBehaviour
{
    public float Range;
    public float Cooldown;
    public float RotateSpeed;
}

public class TowerBaker : Baker<TowerAuthoring>
{
    public override void Bake(TowerAuthoring authoring)
    {
        Entity towerEntity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(towerEntity, new CollisionComponent 
        {
            Radius = authoring.Range
        });

        AddComponent(towerEntity, new LookAtTargetComponent
        {
            RotateSpeed = authoring.RotateSpeed
        });

        AddComponent(towerEntity, new RocketSpawnerComponent
        {
            Cooldown = authoring.Cooldown,
        });

        AddComponent(towerEntity, new TowerDetectionComponent());
    }
}
