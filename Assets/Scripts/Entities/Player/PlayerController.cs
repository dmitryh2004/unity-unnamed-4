using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField] Player player;
    private void Update()
    {
        if (InputManager.Instance.Dash)
        {
            player.AddEffect(EffectID.Dash);
        }

        Vector2 direction = Vector2.zero;
        if (InputManager.Instance != null)
        {
            direction = InputManager.Instance.Movement;
        }
        player.Move(direction);
    }
}
