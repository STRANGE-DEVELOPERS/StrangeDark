using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour
{
    public GameObject[] decorate;
    void Start()
    {
        int randdecorate = Random.Range(0, decorate.Length);
        Instantiate(decorate[randdecorate], transform.position, Quaternion.identity);
    }

    
}
