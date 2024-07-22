using Unity.Entities;
using Unity.Transforms;
using PathCreation;

public partial class TankMovementSystem : SystemBase
{
    public PathCreator Path;

    protected override void OnUpdate()
    {
        if(GameManager.Instance != null)
        {
            if(GameManager.Instance.IsGameOver)
            {
                return;
            }
        }

        if(Path == null)
            return;
        float deltaTime = SystemAPI.Time.DeltaTime;
        Entities
        .ForEach((ref LocalTransform transform, ref TankMovementComponent movement, in TankHealthComponent health) => 
        {
            movement.DistanceTravelled += deltaTime * movement.Speed;
            transform.Position = Path.path.GetPointAtDistance(movement.DistanceTravelled, EndOfPathInstruction.Stop);
            transform.Rotation = Path.path.GetRotationAtDistance(movement.DistanceTravelled, EndOfPathInstruction.Stop);
        })
        .WithoutBurst()
        .Run();
    }
}
