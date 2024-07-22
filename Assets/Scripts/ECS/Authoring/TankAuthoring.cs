using UnityEngine;
using Unity.Entities;

public class TankAuthoring : MonoBehaviour
{
    public int Health;
    public int Speed;
    public float CollisionRadius;
}

public class TankBaker : Baker<TankAuthoring>
{
    public override void Bake(TankAuthoring authoring)
    {
        Entity tankEntity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(tankEntity, new TankHealthComponent 
        {
            Value = authoring.Health
        });

        AddComponent(tankEntity, new TankMovementComponent 
        {
            Speed = authoring.Speed,
            DistanceTravelled = 0.0f,
        });

        AddComponent(tankEntity, new CollisionComponent
        {
            Radius = authoring.CollisionRadius
        });

        AddComponent(tankEntity, new TankTag());
    }
}
