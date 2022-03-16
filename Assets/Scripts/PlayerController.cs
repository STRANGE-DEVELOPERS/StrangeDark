using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Creating variables for the HP band
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject flask, FirstAidKit;

    //Creating variables for weapons
    [Header("Weapon")]
    [SerializeField] private List<GameObject> Weapons;
    [SerializeField] private GameObject[] allWeapon;

    public Joystick joystick;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    float offset;

    Vector2 movement;
    public static bool _flipRight = true;
    private Animator animator;

    // Handle event
    public delegate void WeaponChange(GameObject newWeapon);
    public static event WeaponChange WeaponUpdated; 

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        movement = new Vector2(joystick.Horizontal, joystick.Vertical);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Flask")
        {
            IncreaseHealth(30);
            Destroy(flask);
        }
        else if (collision.CompareTag ("FirstAidKit"))
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            Destroy(FirstAidKit);
        }

        else if (collision.tag == "Enemy")
        {
            TakeDemage(30);
        }

        else if (collision.CompareTag("Weapon"))
        {
            for (int i =0; i< allWeapon.Length; i++)
            {
                if (collision.name == allWeapon[i].name)
                {
                    Weapons.Add(allWeapon[i]);
                    break;
                }
            }
            SwitchWeapon();
            Destroy(collision.gameObject);
        }
   }

    void TakeDemage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
        
    void IncreaseHealth(int healt)
    {
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healt;
            if (currentHealth> maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        healthBar.SetHealth(currentHealth);
    }


    public void SwitchWeapon()
    {
        for (int i=0; i< Weapons.Count; i++)
        {
            if( Weapons[i].activeInHierarchy )
            {
                Weapons[i].SetActive(false);


                if (i != 0)
                {
                    Weapons[i - 1].SetActive(true);

                    if (WeaponUpdated != null)
                        WeaponUpdated(Weapons[i - 1]);

                }
                else
                {
                    Weapons[Weapons.Count - 1].SetActive(true);
                    if (WeaponUpdated != null)
                        WeaponUpdated(Weapons[Weapons.Count - 1]);
                }


                break;
            }
        }
    }
}