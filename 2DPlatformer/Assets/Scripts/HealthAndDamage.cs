using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndDamage : MonoBehaviour
{
    public int currentHealth = 10;
    public int maxHealth = 10;


    public List<string> tagsThatDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth == 0)
        {
            //dead
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
