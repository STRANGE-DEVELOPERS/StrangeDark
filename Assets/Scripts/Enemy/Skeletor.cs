using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletor : Enemy
{
    [SerializeField] float skeletorSpeed;
    [SerializeField] float skeletorSpeedBoost;
    [SerializeField] float skeletorAggroDistance;
    [SerializeField] int skeletorHealth;

    private float damage;

    private void Awake()
    {
        speed = skeletorSpeed;
        speedBoost = skeletorSpeedBoost;
        aggroDistance = skeletorAggroDistance;
        maxHealth = skeletorHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            CheckAggro();
            if (isAggro)
                FollowTarget();
            Attack();
        }
    }


    void Attack()
    {
        if (reachedTarget)
            animator.SetInteger("State", 2);
        else
            animator.SetInteger("State", 1);
    }
}
