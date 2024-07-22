using UnityEngine;
using Unity.Entities;

public class RocketAuthoring : MonoBehaviour
{
    public float CollisionRadius;
    public int Damage;
    public float MoveSpeed;
    public float RotateSpeed;
    public float MaxFlightDistance;
}

public class RocketBaker : Baker<RocketAuthoring>
{
    public override void Bake(RocketAuthoring authoring)
    {
        Entity rocketEntity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(rocketEntity, new CollisionComponent
        {
            Radius = authoring.CollisionRadius
        });

        AddComponent(rocketEntity, new DamageComponent
        {
            Value = authoring.Damage
        });

        AddComponent(rocketEntity, new LookAtTargetComponent
        {
            RotateSpeed = authoring.RotateSpeed
        });

        AddComponent(rocketEntity, new RocketMovementComponent
        {
            Speed = authoring.MoveSpeed,
            MaxRange = authoring.MaxFlightDistance,
        });

        AddComponent(rocketEntity, new RocketTag());

        AddComponent(rocketEntity, new RocketTargetDetectionComponent());
    }
}
