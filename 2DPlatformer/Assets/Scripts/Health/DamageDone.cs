using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDone : MonoBehaviour
{
    public int damage = 0;
    public float knockback = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthAndDamage health = collision.GetComponent<HealthAndDamage>();
        if(health != null)
            health.TryTakeDamage(gameObject.tag, damage, knockback, collision.transform.position);
    }
}
