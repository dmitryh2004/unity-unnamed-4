using UnityEngine;

public class EntitySpriteController : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    [SerializeField] int spritesInRow, spritesInColumn;
    [SerializeField] SpriteRenderer spriteRenderer;
    Material material;

    private void Start()
    {
        material = spriteRenderer.material;
        material.SetTexture("_MainTex", sprite.texture);

        // float spriteWidth = 1f / spritesInRow;
        // float spriteHeight = 1f / spritesInColumn;

        // material.SetVector("_UVRemap", new Vector4(spritesInRow, spritesInColumn, spriteWidth, spriteHeight));
        material.SetFloat("decayDegree", 0f);
        Apply();
    }

    private void LateUpdate()
    {
        if (spriteRenderer.sprite != sprite)
            Apply();
    }

    void Apply()
    {
        sprite = spriteRenderer.sprite;
        if (sprite == null) return;

        Rect r = sprite.textureRect;
        Texture2D tex = sprite.texture;
        Vector4 uvRemap = new Vector4(
            r.x / tex.width,
            r.y / tex.height,
            r.width / tex.width,
            r.height / tex.height
        );

        material.SetVector("_UVRemap", uvRemap);
    }
}
