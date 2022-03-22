using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    public int openingDirection;
       // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door


    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //Invoke("Spawn", 0.1f);
        Spawn();
        Debug.Log("Finished spawning rooms");
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.bottonRooms.Length);
                Instantiate(templates.bottonRooms[rand], transform.position, templates.bottonRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                ////Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                //// Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint") && other.GetComponent<RoomSpawn>().spawned)
        {
            Destroy(gameObject);
        }

    }

}
