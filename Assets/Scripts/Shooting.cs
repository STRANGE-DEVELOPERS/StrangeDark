using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    PlayerController player;

    private Transform weapon;

    private float aimAngle = 0;
    public Camera cam;
    public float bulletForce=20f;

    bool playerLooksRight = true;

    private void Awake()
    {
        weapon = transform.Find("Gun");
        cam = Camera.main;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        AimWeapon();
        Shoot();

    }

    void Shoot ()
    {
        if (Input.GetButtonDown("Fire1"))
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
        Vector3 mousePos = GetMouseWorldPosition(Input.mousePosition);

        Vector3 aimDirection = (mousePos - transform.position).normalized;
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x);
        float aimAngleDeg = aimAngle * Mathf.Rad2Deg;
        weapon.eulerAngles = new Vector3(0, 0, aimAngleDeg);

        firePoint.position = new Vector3(Mathf.Cos(aimAngle), Mathf.Sin(aimAngle), firePoint.position.z) + weapon.position;

        Vector3 aimLocal = weapon.localScale;
        if (aimAngleDeg > 90 || aimAngleDeg < -90)
        {
            if (playerLooksRight)
            {
                aimLocal.x = -1f;
                aimLocal.y = -1f;
                player.Flip(playerLooksRight);
                playerLooksRight = !playerLooksRight;
            }
        }
        else
        {
            if (!playerLooksRight)
            {
                aimLocal.x = +1f;
                aimLocal.y = +1f;
                player.Flip(playerLooksRight);
                playerLooksRight = !playerLooksRight;
            }
        }
        weapon.localScale = aimLocal;
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
        Vector2 theScale = player.transform.localScale;
        theScale.x *= -1;
        player.transform.localScale = theScale;

        playerLooksRight = !playerLooksRight;
    }
}
