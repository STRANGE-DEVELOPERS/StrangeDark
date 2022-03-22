using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float speed;
    protected float speedBoost;
    protected float aggroDistance;

    protected int damage;

    private int currentHealth;
    protected int maxHealth;

    protected Animator animator;
    private Rigidbody2D rb2D;

    protected GameObject player;

    protected List<Vector3> pathToTarget;
    protected Vector3 currentPathTarget;

    // change this to state of the AI state machune later
    protected bool isAggro = false;
    bool facesRight = true;
    protected bool reachedTarget = false;
    protected bool isAlive = true;
    private int deathTimer = 100;

    protected void Start()
    {   
        player = GameObject.FindWithTag("Player");
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isAlive)
            Dies();
    }

    #region Movement

    protected void SetTarget(Vector3 target)
    {
        Debug.Log("Trying to fing path from " + transform.position.x + " to " + target.x);
        pathToTarget = Pathfinding2D.Instance.FindPath(transform.position, target);

        if (pathToTarget != null && pathToTarget.Count > 1)
        {
            currentPathTarget = pathToTarget[1];
        }
        else
            pathToTarget = null;
    }

    protected void FollowTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            reachedTarget = true;
            return;
        }            

        if (pathToTarget == null)
            SetTarget(player.transform.position);

        float distanceToTarget = Vector3.Distance(transform.position, currentPathTarget);
        if (distanceToTarget > 1f)
        {
            Vector3 moveDirection = (currentPathTarget - transform.position).normalized;
            transform.position = transform.position + moveDirection * speed * Time.deltaTime;
            MovingManageVisual(moveDirection.x);
            reachedTarget = false;
        }
        else
        {
            SetTarget(player.transform.position);
        }
    }

    private void MovingManageVisual(float xVel)
    {
        if (xVel < 0 && facesRight)
        {
            Flip();
        }
        if (xVel > 0 && !facesRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facesRight = !facesRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    #endregion Movement

    protected void CheckAggro()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= aggroDistance && !isAggro)
        {
            isAggro = true;
        }
    }

    public void TakeDamage(int damage)
    {
        // stopTime = StartStopTime;
        if (currentHealth - damage <= 0)
        {
            isAlive = false;
        }
        else 
        {
            animator.SetTrigger("IsHit");
            currentHealth -= damage;
        }
    }

    protected void DealDamage()
    {
        player.GetComponent<PlayerController>().TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            int damageTaken = player.GetComponent<PlayerController>().DeliverDamage();
            TakeDamage(damageTaken);
        }
    }

    private void Dies()
    {
        animator.SetTrigger("Dies");

        if (deathTimer > 0)
        {
            deathTimer--;
        }
        else
        {
            DropItem.Instance.SpawnItem(this.transform.position);
            Destroy(gameObject);
        }
    }
}