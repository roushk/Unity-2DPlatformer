using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerMusic : MonoBehaviour
{
    public float combatMusicMinRadius = 3.6f;
    public float combatMusicMaxRadius = 5.0f;

    BackgroundMusicManager musicManager;
    
    bool combatMusicIsPlaying = false;
    float updateFreqMax = 1.0f;
    float updateFreq = 0;

    private LayerMask enemyLayerMask;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, combatMusicMinRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, combatMusicMaxRadius);
    }
    // Start is called before the first frame update
    void Start()
    {
        //easiest way is to just link it to the only gameobj named audio manager because ref's break over level transitions
        musicManager = GameObject.Find("AudioManager").GetComponent<BackgroundMusicManager>();
        enemyLayerMask = LayerMask.GetMask("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        //dont check every single frame
        updateFreq += Time.deltaTime;
        if (updateFreq >= updateFreqMax)
        {
            updateFreq = 0.0f;
            bool inCombatThisFrameInnerRadius = false;
            bool inCombatThisFrameOuterRadius = false;

            //Check inner radius for enemies
            Collider2D[] overlappedObjects = Physics2D.OverlapCircleAll(transform.position, combatMusicMinRadius, enemyLayerMask);

            foreach (var overlappedObj in overlappedObjects)
            {
                //"quick" recursive check if an enemy and if so deeper check if has wolf AI aka is a wolf and only if that wolf isn't aggroed
                if (overlappedObj.gameObject.GetComponent<HealthAndDamage>() != null && overlappedObj.gameObject.GetComponent<HealthAndDamage>().isDead == false)
                {
                    inCombatThisFrameInnerRadius = true;
                    break;

                }
            }

            //make sure no enemies are in the outer radius
            overlappedObjects = Physics2D.OverlapCircleAll(transform.position, combatMusicMaxRadius, enemyLayerMask);

            foreach (var overlappedObj in overlappedObjects)
            {
                //"quick" recursive check if an enemy and if so deeper check if has wolf AI aka is a wolf and only if that wolf isn't aggroed
                if (overlappedObj.gameObject.GetComponent<HealthAndDamage>() != null && overlappedObj.gameObject.GetComponent<HealthAndDamage>().isDead == false)
                {
                    inCombatThisFrameOuterRadius = true;
                    break;
                }
            }

            //update the music
            if (inCombatThisFrameInnerRadius)
            {
                //only fade to combat if we are in combat this frame and are currently playing combat music
                if (combatMusicIsPlaying == false)
                {
                    musicManager.PlayCombatMusic();
                    combatMusicIsPlaying = true;
                }
            }
            else if(inCombatThisFrameOuterRadius == false)
            {
                //only fade to exploration if we are not in combat this frame and are currently playing combat music
                if(combatMusicIsPlaying == true)
                {
                    musicManager.PlayExplorationMusic();
                    combatMusicIsPlaying = false;
                }
            }
        }
    }

  
}
