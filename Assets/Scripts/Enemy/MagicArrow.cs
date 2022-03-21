using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : Enemy
{
    [SerializeField] private int magicDamage;
    [SerializeField] private float spellDuration;
    [SerializeField] private float arrowSpeed;


    private void Start()
    {
        base.Start();
        StartCoroutine(Expire());
        SetTarget(player.transform.position);
        speed = arrowSpeed;
        damage = magicDamage;

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        FlyToPlayer();
        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            DealDamage();
            StopCoroutine(Expire());
            Debug.Log("Magic bolt deals damage");
            Destroy(gameObject);
        }
    }

    IEnumerator Expire()
    {
        yield return new WaitForSeconds(spellDuration);

        Destroy(gameObject);
    }

    void FlyToPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 0.5f)
        {
            reachedTarget = true;
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, currentPathTarget);
        if (distanceToTarget > 0.5f)
        {
            Vector3 moveDirection = (currentPathTarget - transform.position).normalized;
            SetAngle(moveDirection);
            transform.position = transform.position + moveDirection * speed * Time.deltaTime;
            reachedTarget = false;
        }
        else
        {
            SetTarget(player.transform.position);
        }
    }


    void SetAngle(Vector3 direction)
    {
        Vector3 angleDirection = (direction).normalized;
        float aimAngle = Mathf.Atan2(direction.y, direction.x);
        float aimAngleDeg = aimAngle * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, aimAngleDeg);
    }
}
