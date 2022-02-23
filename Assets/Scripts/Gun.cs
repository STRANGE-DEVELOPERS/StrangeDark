using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg ;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);
    }
}
