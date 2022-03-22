using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private CameraController cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            cam.minValue += cameraChangePos;
            cam.maxValue += cameraChangePos;
            collision.transform.position += playerChangePos;
        }
    }
}
