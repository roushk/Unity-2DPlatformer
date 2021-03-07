using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public GameObject healthBar;
    public GameObject healthBarMask;

    public Gradient healthGradient;

    public HealthAndDamage obj;

    private SpriteRenderer healthBarSprite;
    private Transform healthBarTransform;
    private Transform healthBarMaskTransform;

    //The position of the mask that makes the health bar just empty
    public float xZeroHp = -3.63f;
    public float xMaxHp = 0f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        healthBarSprite = healthBar.GetComponent<SpriteRenderer>();
        healthBarTransform = healthBar.GetComponent<Transform>();
        healthBarMaskTransform = healthBarMask.GetComponent<Transform>();

        if(obj == null)
        {
            obj = GetComponentInParent<HealthAndDamage>();
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(obj.isDead)
        {
            gameObject.SetActive(false);
        }

        float t = (float)obj.currentHealth / (float)obj.maxHealth;

        //set the color to the gradient evaluated at the current/max so 1 = green and 0 = red
        healthBarSprite.color = healthGradient.Evaluate(t);

        //move the health bar mask left
        healthBarMaskTransform.localPosition = new Vector3(Mathf.Lerp(xZeroHp, xMaxHp, t), 0.06f, 0);
    }
}
