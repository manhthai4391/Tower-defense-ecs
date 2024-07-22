using Unity.Entities;
using Unity.Transforms;

[UpdateAfter(typeof(TowerTargetDetectionSystem))]
[UpdateAfter(typeof(TankCollisionSystem))]

public partial class RocketTargetDetectionSystem : SystemBase
{
    protected override void OnUpdate()
    {
        if(GameManager.Instance != null)
        {
            if(GameManager.Instance.IsGameOver)
            {
                return;
            }
        }

        Dependency = Entities.ForEach((Entity entity, ref LookAtTargetComponent lookAt, ref RocketMovementComponent movement, in RocketTargetDetectionComponent detection, in CollisionComponent collision) => 
        {
            Entity target = SystemAPI.GetComponent<TowerDetectionComponent>(detection.SpawnTower).Target;           

            if(target == Entity.Null)
            {
                lookAt.ShouldLook = false;
                movement.ShouldMove = false;
                return;
            }

            LocalTransform targetTransform = SystemAPI.GetComponent<LocalTransform>(target);
            
            lookAt.ShouldLook = true;
            lookAt.TargetPosition = targetTransform.Position;
            movement.ShouldMove = true;
            
        })
        .WithBurst()
        .ScheduleParallel(Dependency);
    }
}
