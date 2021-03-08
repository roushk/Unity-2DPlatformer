using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterTintShaderInjector : MonoBehaviour
{
    public Color tint = new Color(0,0,0);

    private MaterialPropertyBlock materialPropertyBlock;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetVector("_Tint", tint);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
