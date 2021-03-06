using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxValueScript : MonoBehaviour
{
    //Generally y = 0.2 * X speed
    public Vector2 scrollSpeed = new Vector2(100,20);

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
        materialPropertyBlock.SetVector("_ScrollSpeed", scrollSpeed);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
