using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxValueScript : MonoBehaviour
{
    public float scrollSpeed = 50;
    private MaterialPropertyBlock materialPropertyBlock;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //materialPropertyBlock.SetFloat("ScrollSpeed", scrollSpeed);
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();

        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat("_ScrollSpeed", scrollSpeed);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
