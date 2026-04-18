using UnityEngine;
using Pathfinding;

public class Enemy : Entity
{
    protected Path path;
    protected Seeker seeker;
    protected AIPath aiPath;
    protected AIDestinationSetter destinationSetter;

    protected virtual void Start()
    {
        Debug.Log(animator);
        if (!gameObject.TryGetComponent<Seeker>(out seeker))
        {
            Debug.LogError(gameObject.name + ": missing Seeker component");
        }
        if (!gameObject.TryGetComponent<AIPath>(out aiPath))
        {
            Debug.LogError(gameObject.name + ": missing AIPath component");
        }
        if (!gameObject.TryGetComponent<AIDestinationSetter>(out destinationSetter))
        {
            Debug.LogError(gameObject.name + ": missing AIDestinationSetter component");
        }
        if (seeker)
        {
            path = seeker.StartPath(transform.position, destinationSetter.target.transform.position);
        }
    }

    protected void UpdateSpeed()
    {
        SetMoving(destinationSetter.target != null);

        aiPath.maxSpeed = IsMoving() ? GetCurrentSpeed() : 0;
        SetDirection(aiPath.desiredVelocity);

        Debug.Log($"{gameObject.name} - direction: {CurrentDirection}");

        UpdateMoveAnimator();
    }

    protected override void UpdateEntity()
    {
        base.UpdateEntity();
        UpdateSpeed();
    }
}
