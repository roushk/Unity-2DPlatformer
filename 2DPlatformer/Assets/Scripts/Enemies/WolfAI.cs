using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    public float aggroRadius = 1.0f;
    public float deAggroRadius = 2.0f;   //if you leave this the wolves stop being angry and go back to their original position
    public float deAggroTime = 2.0f;    //time to wait to move back to original pos after deaggroing.

    //incremented time
    float deAggroCurrentTime = 1.0f;

    private Vector3 origPos;
    private float aggroMovementSpeed;
    private float deAggroMovementSpeed;

    public float otherWolfAggroRadius = 1f;
    public float attackRadius = 0.3f;

    public float attackCooldown = 0.5f;

    public PlayRandomSound growlSoundList;
    public PlayRandomSound takeDamageSoundList;
    public PlayRandomSound deathSoundList;
    public PlayRandomSound fellowAggroSoundList;

    bool isAggro = false;
    bool justAggro = false;

    bool firstFrameDead = true;

    private Transform target;
    private WolfMovement movement;
    private Animator animator;
    private HealthAndDamage health;

    private LayerMask wolfLayerMask;

    IEnumerator SetOriginalPos()
    {
        yield return new WaitForSeconds(0.2f);
        origPos = transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        movement = GetComponent<WolfMovement>();
        health = GetComponent<HealthAndDamage>();

        aggroMovementSpeed = movement.runSpeed;
        deAggroMovementSpeed = movement.runSpeed * 0.5f;

        wolfLayerMask = LayerMask.GetMask("Enemies");

        StartCoroutine(SetOriginalPos());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, deAggroRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, otherWolfAggroRadius);
    }

    void Update()
    {
        deAggroCurrentTime += Time.deltaTime;

        if (health.isDead)
        {
            if(firstFrameDead)
            {
                deathSoundList.PlaySound();
                firstFrameDead = false;
            }

            //stop dead wolfs jumping
            movement.moveTowardPos = transform.position;
            return;
        }

        if(health.justTakenDamage)
        {
            takeDamageSoundList.PlaySound();
        }

        //Maybe use 1d but breaks if wolf is above/below and in aggro range
        float distToPlayerX = Vector3.Distance(transform.position,target.position);

        //basic aggro radius 
        if(distToPlayerX <= aggroRadius)
        {
            isAggro = true;
        }

        //deaggro radius much larger than regular radius
        if (distToPlayerX > deAggroRadius)
        {
            isAggro = false;
        }

        if(isAggro)
        {
            deAggroCurrentTime = 0; //reset time
        }

        if (isAggro && justAggro == false)
        {
            justAggro = true;
        }

        else if(isAggro == false && justAggro == true)
        {
            justAggro = false;
        }

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        if(isAggro && !info.IsName("WolfAttack"))
        {
            movement.moveTowardPos = target.position;
        }
        else if(!isAggro && deAggroCurrentTime > deAggroTime)
        {
            movement.moveTowardPos = origPos;
        }
        else
        {
            movement.moveTowardPos = transform.position;
        }

        if (distToPlayerX < attackRadius && !info.IsName("WolfAttack"))
        {
            animator.SetTrigger("WolfAttack");
            growlSoundList.PlaySound();
        }

        //first time aggro'd then anger other wolves in the area
        //TODO make wolf howl noise from a set of multiple wolves aggroed
        if(justAggro)
        {
            //if the func found any other wolves that aren't aggroed then howl
            if(TriggerWolvesInRadius())
            {
                fellowAggroSoundList.PlaySound(0.5f);
            }
        }

        //TODO remove this hacky bs or add walking anim
        //ok this actually looks and works very well tbh
        if(isAggro)
        {
            animator.speed = 1;
            movement.runSpeed = aggroMovementSpeed;
        }
        else if(!isAggro && deAggroCurrentTime > deAggroTime)
        {
            animator.speed = 0.5f;
            movement.runSpeed = deAggroMovementSpeed;
        }
    }

    public bool TriggerWolvesInRadius()
    {
        bool triggeredOtherWolves = false;
        Collider2D[] overlappedObjects = Physics2D.OverlapCircleAll(transform.position, otherWolfAggroRadius, wolfLayerMask);

        foreach (var collider in overlappedObjects)
        {
            //"quick" recursive check if an enemy and if so deeper check if has wolf AI aka is a wolf and only if that wolf isn't aggroed
            if (collider.gameObject.GetComponent<WolfAI>() != null && collider.gameObject.GetComponent<WolfAI>().isAggro == false)
            {
                triggeredOtherWolves = true;

                collider.gameObject.GetComponent<WolfAI>().isAggro = true;
                collider.gameObject.GetComponent<WolfAI>().TriggerWolvesInRadius();

            }
        }
        return triggeredOtherWolves;
    }
}
