using UnityEngine;

public class Player : Entity
{

    protected override void OnDeath()
    {
        Debug.Log("You died!");
    }
}
