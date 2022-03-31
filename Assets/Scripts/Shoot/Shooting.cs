using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public Joystick joystick;

    PlayerController player;

    private GameObject weapon;

    private float aimAngle = 0;
    public Camera cam;
    public float bulletForce = 20f;

    public float fireRate = 0.4f; // in seconds

    bool playerLooksRight = true;

    private void Awake()
    {
        weapon = GameObject.Find("Gun");
        cam = Camera.main;
        joystick = GameObject.Find("Shooting Joystick").GetComponent<Joystick>();

        player = GameObject.Find("Player").GetComponent<PlayerController>();

        PlayerController.WeaponUpdated += SwitchWeapon;
    }

    public void Start()
    {
        StartCoroutine(FireRateCoroutine());
    }

    void Update()
    {
        AimWeapon();
    }

    void Shoot()
    {
        // if (Input.GetButtonDown("Fire1"))
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            float xComponent = Mathf.Cos(aimAngle) * bulletForce;
            float zComponent = Mathf.Sin(aimAngle) * bulletForce;

            Vector2 forceApplied = new Vector2(xComponent, zComponent);
            rb.AddForce(forceApplied, ForceMode2D.Impulse);
        }
    }

    private void AimWeapon()
    {
        // Vector3 mousePos = GetMouseWorldPosition(Input.mousePosition);
        Vector3 moveVector = (Vector3.up * joystick.Vertical - Vector3.left * joystick.Horizontal);

        Vector3 aimDirection = (moveVector).normalized;
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x);
        float aimAngleDeg = aimAngle * Mathf.Rad2Deg;
        weapon.transform.eulerAngles = new Vector3(0, 0, aimAngleDeg);

        firePoint.position = new Vector3(Mathf.Cos(aimAngle), Mathf.Sin(aimAngle), firePoint.position.z) + weapon.transform.position;

        Vector3 aimLocal = weapon.transform.localScale;
        if (aimAngleDeg > 90 || aimAngleDeg < -90)
        {
            if (playerLooksRight)
            {
                aimLocal.x = -1f;
                aimLocal.y = -1f;
                player.Flip(false);
                playerLooksRight = false;
                //FlipPlayer();
            }
        }
        else
        {
            if (!playerLooksRight)
            {
                aimLocal.x = +1f;
                aimLocal.y = +1f;
                player.Flip(true);
                playerLooksRight = true;
                //FlipPlayer();
            }
        }
        weapon.transform.localScale = aimLocal;
    }

    // utility method: get mouse world position
    private Vector3 GetMouseWorldPosition(Vector3 screenPosition)
    {
        Vector3 worldPosition = cam.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0f;
        return worldPosition;
    }

    private void FlipPlayer()
    {
        Vector2 theScale = player.transform.transform.localScale;
        theScale.x *= -1;
        player.transform.transform.localScale = theScale;

        playerLooksRight = !playerLooksRight;
    }

    private void SwitchWeapon(GameObject newWeapon)
    {
        weapon = newWeapon;
    }

    IEnumerator FireRateCoroutine()
    {
        while (player.isActiveAndEnabled)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }

    }
}
