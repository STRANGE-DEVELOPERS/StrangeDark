using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField] float wizardAggroDistance;
    [SerializeField] int wizardHealth;
    [SerializeField] float magicAttackCoolDown;
    [SerializeField] float summonCoolDown;
    [SerializeField] GameObject magicArrowPrefab;
    [SerializeField] GameObject skeletorPrefab;

    private float lastAttackedAt = -99999f;
    public float lastSummonedAt = -99999f;

    private void Awake()
    {
        maxHealth = wizardHealth;
        lastSummonedAt = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (PlayerInRange())
            {
                AttackMagic();
                SpawnSkeletor();
            }
            else if (animator.GetBool("IsAttacking"))
                animator.SetBool("IsAttacking", false);
        }
    }

    private bool PlayerInRange()
    {
        return (Vector3.Distance(transform.position, player.transform.position) < wizardAggroDistance);
    }

    void AttackMagic()
    {
        if (Time.time > lastAttackedAt + magicAttackCoolDown)
        {
            //do the attack
            animator.SetBool("IsAttacking", true);
            GameObject magicArrow = Instantiate(magicArrowPrefab, transform.position, Quaternion.identity);

            lastAttackedAt = Time.time;
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    void SpawnSkeletor()
    {
        if (Time.time > lastSummonedAt + summonCoolDown)
        {
            //spawn a skeleton
            animator.SetBool("IsSummoning", true);
            GameObject skeletor = Instantiate(skeletorPrefab, transform.position, Quaternion.identity);

            lastSummonedAt = Time.time;
        }
        else
        {
            animator.SetBool("IsSummoning", false);
        }
    }
}
