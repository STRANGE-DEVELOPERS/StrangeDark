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
    [SerializeField] private int currentDamage;

    //Creating variables for weapons
    [Header("Weapon")]
    [SerializeField] private List<GameObject> Weapons;
    [SerializeField] private GameObject[] allWeapon;

    public Joystick joystick;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    float offset;

    Vector2 movement;
    public bool _flipRight;
    private Animator animator;

    // Handle event
    public delegate void WeaponChange(GameObject newWeapon);
    public static event WeaponChange WeaponUpdated; 

    private void Start()
    {
        //healthBar = GetComponent<HealthBar>();
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
            if (movement.x > 0 && !_flipRight)
            {
                Flip(true);
            }
            if (movement.x < 0 && _flipRight)
            {
                Flip(false);
            };
            animator.SetInteger("Movement", 1);
        }
        else
        {
            animator.SetInteger("Movement", 0);
        }

       
    }
    
    public void Flip(bool playerLooksRight)
    {
        _flipRight = playerLooksRight;
        Vector3 theScale = transform.localScale;

        if (!playerLooksRight)
            theScale.x = -Mathf.Abs(theScale.x);
        else
            theScale.x = Mathf.Abs(theScale.x);

        transform.localScale = theScale;

        // don't flip healthbar
        // healthBar.transform.localScale = new Vector3(Mathf.Abs(healthBar.gameObject.transform.localScale.x), healthBar.gameObject.transform.localScale.y, healthBar.gameObject.transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Flask")
        {
            IncreaseHealth(30);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag ("FirstAidKit"))
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            Destroy(collision.gameObject);
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

    public int DeliverDamage()
    {
        return currentDamage;
    }
}