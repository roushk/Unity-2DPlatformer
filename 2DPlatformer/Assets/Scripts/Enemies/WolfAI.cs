using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    public float aggroRadius = 1;
    public float attackRadius = 0.3f;

    public float attackCooldown = 0.5f;

    public PlayRandomSound growlSoundList;
    public PlayRandomSound takeDamageSoundList;
    //public PlayRandomSound growlSoundList;


    bool isAggro = false;
    bool justAggro = false;

    private Transform target;
    private WolfMovement movement;
    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        movement = GetComponent<WolfMovement>();
    }

    void FixedUpdate()
    {
        //Maybe use 1d but breaks if wolf is above/below and in aggro range
        float distToPlayerX = Vector3.Distance(transform.position,target.position);

        //simple 1d radius
        isAggro = distToPlayerX < aggroRadius;

        if(isAggro && justAggro == false)
        {
            //Just aggrod's stuff here
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
        else
        {
            movement.moveTowardPos = transform.position;
        }

        if(distToPlayerX < attackRadius && !info.IsName("WolfAttack"))
        {
            animator.SetTrigger("WolfAttack");
            growlSoundList.PlaySound();
        }

        //Get health from health component
    }
}
