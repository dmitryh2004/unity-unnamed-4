using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField] Player player;
    private void Update()
    {
        if (InputManager.Instance != null)
        {
            // dash
            if (InputManager.Instance.Dash)
            {
                player.AddEffect(EffectID.Dash);
            }

            // movement
            Vector2 direction = Vector2.zero;

            direction = InputManager.Instance.Movement;
            player.Move(direction);

            // attack
            if (InputManager.Instance.Attack)
            {
                player.TryAttack();
            }
        }
    }
}
