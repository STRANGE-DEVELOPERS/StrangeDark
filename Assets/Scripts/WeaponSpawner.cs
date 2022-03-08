using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] weapons;

    [SerializeField] private float time;

    [SerializeField] private float xBound, yBound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWeapon());
    }

    IEnumerator SpawnWeapon()
    {
        yield return new WaitForSeconds(time);
        Vector2 spawnPoint = new Vector2(Random.Range(-xBound, xBound), Random.Range(-yBound, yBound));
        if (GameObject.FindGameObjectsWithTag("Weapon").Length < 3)
            Instantiate(weapons[Random.Range(0, weapons.Length)], spawnPoint, Quaternion.identity);
        StartCoroutine(SpawnWeapon());

    }
   
}
