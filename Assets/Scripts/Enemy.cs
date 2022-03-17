using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float speedBoost;
    
    [SerializeField] private float distanceAngry;
    
    [SerializeField] private float distancePatrol;

    [SerializeField] private Transform point;

    [SerializeField] private int currentHealth;

    public GameObject lootDrop;

    public DropItem[] dropList;

    private float minDistance;
    private float maxDistance;

    private Animator animator;
    private Rigidbody2D rb2D;

    private bool patrol = true;

    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");

        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        minDistance = transform.position.x - distancePatrol;
        maxDistance = transform.position.x + distancePatrol;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol == true)
            Patrol();
        else
            Angry();
        if (Vector2.Distance(transform.position, player.transform.position)< distanceAngry)
        {
            Mathf.Abs(speed);
            patrol = false;
        }
        
    }

    private void Patrol()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);

        if (transform.position.x > maxDistance)
        {
           speed = -speed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (transform.position.x < minDistance)
        {
            speed = -speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        animator.SetInteger("State", 0);
       
    }

    private void Angry()
    {
        if (patrol == false)
        {
            Vector2 moveVector = Vector2.MoveTowards(transform.position, player.transform.position, speedBoost * speed * Time.deltaTime);
            transform.position = new Vector2(moveVector.x, transform.position.y);
            if (transform.position.x > player.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            else if (transform.position.x < player.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        
    }

    private void GoBack()
    {

    }

   public void TakeDemage(int damage)
    {
       // stopTime = StartStopTime;
        currentHealth -= damage;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject, 0.43f);

            Instantiate(lootDrop, transform.position, Quaternion.identity);

            CheckDrop();
        }
       

        //  TakeDemage();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag ("Player"))
        {
            animator.SetInteger("State", 1);
        }
        
    }


    public void CheckDrop()
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
