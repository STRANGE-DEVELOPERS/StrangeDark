using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    float offset;

    Vector2 movement;

    public static bool _flipRight = true;
    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            /*if (movement.x > 0 && !_flipRight)
            {
                Flip(movement.x > 0);
            }
            if (movement.x < 0 && _flipRight)
            {
                Flip(movement.x < 0);
            };*/
            animator.SetInteger("Movement", 1);
        }
        else
        {
            animator.SetInteger("Movement", 0);
        }
    }

    public void Flip(bool isLookingRight)
    {
        _flipRight = isLookingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void TakeDemage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }


}