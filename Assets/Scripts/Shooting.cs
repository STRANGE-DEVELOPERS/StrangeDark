using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    //private GameObject player;
    PlayerController player;

    private Transform weapon;

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
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    private void AimWeapon()
    {
        Vector3 mousePos = GetMouseWorldPosition(Input.mousePosition);

        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weapon.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocal = weapon.localScale;
        if (angle > 90 || angle < -90)
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
