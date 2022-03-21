using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletor : Enemy
{
    [SerializeField] float skeletorSpeed;
    [SerializeField] float skeletorSpeedBoost;
    [SerializeField] float skeletorAggroDistance;
    [SerializeField] int skeletorHealth;
    [SerializeField] float skeletorAttackRate;

    [SerializeField] private int skeletorDamage;

    private void Start()
    {
        base.Start();
        damage = skeletorDamage;
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
        {
            if (animator.GetInteger("State") != 2)
                StartCoroutine(MeleeAttack());
            animator.SetInteger("State", 2);
        }
        else
            animator.SetInteger("State", 1);
    }

    IEnumerator MeleeAttack()
    {
        yield return new WaitForSeconds(skeletorAttackRate * 0.7f);
        while (reachedTarget)
        {
            DealDamage();
            yield return new WaitForSeconds(skeletorAttackRate);
        }

        yield return null;
    }
}
