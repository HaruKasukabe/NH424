using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Camera cam;
    float moveX;
    float moveY;
    public float sensitiveMove = 0.8f;
    public float sensitiveZoom = 10.0f;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float moveX = Input.GetAxis("Mouse X") * sensitiveMove;
            float moveY = Input.GetAxis("Mouse Y") * sensitiveMove;
            //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            cam.transform.localPosition -= new Vector3(moveX, 0.0f, moveY);
        }

        float moveZ = Input.GetAxis("Mouse ScrollWheel") * sensitiveZoom;
        cam.transform.position += cam.transform.forward * moveZ;
    }
}