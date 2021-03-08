using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGradientWithTime : MonoBehaviour
{
    public Gradient gradient;

    public float initialOffset = 0.0f;

    public float loopDuration = 1.0f;
    private float loopCurrentDuration = 0.0f;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        loopCurrentDuration = initialOffset;
    }

    // Update is called once per frame
    void Update()
    {
        loopCurrentDuration += Time.deltaTime;

        if (loopCurrentDuration > loopDuration)
        {
            loopCurrentDuration = 0;
        }

        sprite.color = gradient.Evaluate(loopCurrentDuration / loopDuration);

    }
}
