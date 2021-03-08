using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthAndDamage : MonoBehaviour
{
    public int currentHearts = 0;
    public int currentHealth = 10;
    public int maxHealth = 10;

    public List<string> tagsThatDamage;

    public bool isDead = false;

    Animator animator;
    SpriteRenderer sprite;
    BetterTintShaderInjector tintInjector;
    Prime31.CharacterController2D controller;

    bool isInvincible = false;
    float invincibilityTime = 0;
    float invincibilityTimeMax = 0.1f;    //lowest time in milliseconds between damage taken

    public Color damageColor = Color.red;
    public Color defaultColor = new Color(0,0,0);

    float damageFeedbackTimeMax = 0.2f; //ms of time to run the taking damage visual
    float damageFeedbackTimeCurrent = 0.5f;


    float knockbackResistance = 1.0f;

    [HideInInspector]
    public bool justTakenDamage = false;

    int healthLastFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        controller = GetComponent<Prime31.CharacterController2D>();
        tintInjector = GetComponent<BetterTintShaderInjector>();
        healthLastFrame = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        justTakenDamage = healthLastFrame != currentHealth;

        invincibilityTime += Time.deltaTime;
        damageFeedbackTimeCurrent += Time.deltaTime;

        isInvincible = (invincibilityTime < invincibilityTimeMax);

        if (damageFeedbackTimeCurrent < damageFeedbackTimeMax)
        {
            tintInjector.tint = damageColor;
        }
        else
        {
            tintInjector.tint = defaultColor;
        }

        if (currentHealth <= 0)
        {
            //decrement current hearts and check if still greater than zero and if so lose a heart and continue
            if(--currentHearts >= 0)
            {
                currentHealth = maxHealth;
            }
            if(currentHearts < 0 && currentHealth <= 0)
            {
                isDead = true;
                animator.SetBool("IsDead", true);
            }
        }
        else if(justTakenDamage)
        {
            //
        }
        healthLastFrame = currentHealth;

    }

    //reset the just taken damage late
    private void LateUpdate()
    {
        justTakenDamage = false;
    }

    public void TryTakeDamage(string tag, int damage, float knockback, Vector3 otherObjectPos)
    {
        if (tagsThatDamage.Contains(tag) && !isInvincible)
        {
            currentHealth -= damage;

            //reset invincibility timer to stop taking lots of damage at once
            invincibilityTime = 0;

            //reset damage time
            damageFeedbackTimeCurrent = 0;

            justTakenDamage = true;

            //AB = B-A
            Vector3 knockbackDir = Vector3.Normalize(transform.position - otherObjectPos);

            //knockback
            controller.velocity += knockbackDir * (knockback / knockbackResistance);
            
        }
    }

}
