using UnityEngine;

public class EntitySpriteController : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    [SerializeField] int spritesInRow, spritesInColumn;
    [SerializeField] Renderer spriteRenderer;
    Material material;

    private void Start()
    {
        material = spriteRenderer.material;
        material.SetTexture("_MainTex", sprite.texture);

        float spriteWidth = 1f / spritesInRow;
        float spriteHeight = 1f / spritesInColumn;

        material.SetVector("_UVRemap", new Vector4(spritesInRow, spritesInColumn, spriteWidth, spriteHeight));
        material.SetFloat("decayDegree", 0f);
    }
}
