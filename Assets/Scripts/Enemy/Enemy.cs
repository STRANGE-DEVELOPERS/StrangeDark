using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float speed;
    protected float speedBoost;
    protected float aggroDistance;

    private Vector3 startPosition;

    private int currentHealth;

    private GameObject lootDrop;

    private DropItem[] dropList;

    private float minDistance;
    private float maxDistance;

    protected Animator animator;
    private Rigidbody2D rb2D;

    private bool patrol = true;

    protected GameObject player;

    List<Vector3> pathToTarget;
    Vector3 currentPathTarget;

    // change this to state of the AI state machune later
    protected bool isAggro = false;
    bool facesRight = true;
    protected bool reachedTarget = false;

    void Start()
    {
        startPosition = transform.position;
        player = GameObject.Find("Player");

        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    #region Movement

    private void SetTarget(Vector3 target)
    {
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

    private void TakeDemage(int damage)
    {
        // stopTime = StartStopTime;
        currentHealth -= damage;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject, 0.43f);
            Instantiate(lootDrop, transform.position, Quaternion.identity);
            CheckDrop();
        }
       */

        //  TakeDemage();

    }

    private void CheckDrop()
    {
        if (dropList.Length > 0)
        {
            int rnd = (int)Random.Range(0, 100);

            foreach (var item in dropList)
            {
                if (item.chance < rnd)
                {
                    item.CreateDropItem(gameObject.transform.position);
                    return;
                }
            }
        }
    }
}