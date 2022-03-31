using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject [] Enemy;
    [SerializeField] private Transform[] EnemySpawnerPosition;
    private int _randomSpawnPointts;
    public float RepeatRate = 3f;
    public int DestroySpawner = 20;
    private int randEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, RepeatRate);
            Destroy(gameObject, DestroySpawner);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void EnemySpawner()
    {
        randEnemy = Random.Range(0, Enemy.Length);
        _randomSpawnPointts = Random.Range(0, EnemySpawnerPosition.Length);
        Instantiate(Enemy[randEnemy], EnemySpawnerPosition[_randomSpawnPointts].position, Quaternion.identity);
    }



}
