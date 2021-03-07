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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
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
    }

    public void TryTakeDamage(string tag, int damage)
    {
        if (tagsThatDamage.Contains(tag))
        {
            currentHealth -= damage;
        }
    }

}
