using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Camera cam;
    float sensitiveMove;
    float sensitiveZoom;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        sensitiveMove = 0.8f;
        sensitiveZoom = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float moveX = Input.GetAxis("Mouse X") * sensitiveMove;
            float moveY = Input.GetAxis("Mouse Y") * sensitiveMove;
            cam.transform.localPosition -= new Vector3(moveX, 0.0f, moveY);
        }

        float moveZ = Input.GetAxis("Mouse ScrollWheel") * sensitiveZoom;
        cam.transform.position += cam.transform.forward * moveZ;
    }
}
