using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletor : Enemy
{
    [SerializeField] float skeletorSpeed;
    [SerializeField] float skeletorSpeedBoost;
    [SerializeField] float skeletorAggroDistance;

    private float damage;

    private void Awake()
    {
        speed = skeletorSpeed;
        speedBoost = skeletorSpeedBoost;
        aggroDistance = skeletorAggroDistance;
    }
    // Update is called once per frame
    void Update()
    {
        CheckAggro();
        if (isAggro)
            FollowTarget();
        Attack();
    }


    void Attack()
    {
        if (reachedTarget)
            animator.SetInteger("State", 2);
        else
            animator.SetInteger("State", 1);
    }
}
