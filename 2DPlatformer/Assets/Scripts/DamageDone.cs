using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDone : MonoBehaviour
{
    public int damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<HealthAndDamage>().TryTakeDamage(gameObject.tag, damage);
    }
}
