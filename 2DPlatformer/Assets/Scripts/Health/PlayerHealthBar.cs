using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keep the same functionality as HealthBar but adding the new hearts mechanics
public class PlayerHealthBar : HealthBar
{

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        switch(obj.currentHearts)
        {
            case 2:
            {
                heart3.SetActive(false);
                break; 
            }
            case 1:
            {
                heart3.SetActive(false);
                heart2.SetActive(false);
                break;
            }
            case 0:
            {
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                break;
            }
            default:
            break;

        }

    }
}
