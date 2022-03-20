using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 minValue, maxValue;

    [SerializeField] Tilemap level;

    // Update is called once per frame
    private void Start()
    {
        Bounds bound = level.localBounds;
        minValue = bound.min;
        maxValue = bound.max;

        minValue.x += Camera.main.orthographicSize * Screen.width / Screen.height;
        minValue.y += Camera.main.orthographicSize;

        maxValue.x -= Camera.main.orthographicSize * Screen.width / Screen.height;
        maxValue.y -= Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 characterPosition = target.position;

        //Keeping the camera within the level bounds
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(characterPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(characterPosition.y, minValue.y, maxValue.y),
            0);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition + offset;
    }
}
