using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public Shooting weapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Player")
        {
            //collision.GetComponent<PlayerController>().currentWeapon = weapon;
            //collision.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = weapon.Sooting;

          //  Destroy(gameObject);
        }
    }
}
