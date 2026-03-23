using UnityEngine;

public interface IMovable
{
    public void Move(Vector2 direction);
    public float GetCurrentSpeed();
    public float GetBaseSpeed();
}
