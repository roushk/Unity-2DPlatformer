using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScrollShaderInjector : MonoBehaviour
{
    public Vector2 timeScaling = new Vector2(0, 0);

    private Vector2 offset = new Vector2(0,0);
    private float timePassed = 0;

    private MaterialPropertyBlock materialPropertyBlock;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        offset = timePassed * timeScaling;

        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetVector("_Offset", offset);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
