using Unity.Entities;

public struct TowerDetectionComponent : IComponentData
{
    public Entity Target;
    public bool IsTargetInRange;
}
