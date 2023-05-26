using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Camera cam;
    float moveX;
    float moveY;
    public float sensitiveMoveMouse = 0.8f;
    public float sensitiveZoomMouse = 10.0f;

    public float sensitiveMovePad = 0.1f;
    public float sensitiveZoomPad = 2;

    public GameObject LRUI;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.bMenuDisplay())    // ‰½‚©ƒƒjƒ…[‚ð•\Ž¦‚µ‚Ä‚¢‚é‚©‚Ç‚¤‚©
        {
            if (Input.GetMouseButton(1))
            {
                float moveX = Input.GetAxis("Mouse X") * sensitiveMoveMouse;
                float moveY = Input.GetAxis("Mouse Y") * sensitiveMoveMouse;
                //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                cam.transform.localPosition -= new Vector3(moveX, 0.0f, moveY);
            }

            float moveXSti = Input.GetAxis("R_Stick_H") * sensitiveMovePad;
            float moveYSti = Input.GetAxis("R_Stick_V") * sensitiveMovePad;
            //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            cam.transform.localPosition += new Vector3(moveXSti, 0.0f, moveYSti);


            float moveZ = Input.GetAxis("Mouse ScrollWheel") * sensitiveZoomMouse;
            cam.transform.position += cam.transform.forward * moveZ;

            float trigger = Input.GetAxis("LRTrigger");
            if (trigger > 0)
                cam.transform.position += cam.transform.forward * sensitiveZoomPad / 100;
            else if (trigger < 0)
                cam.transform.position -= cam.transform.forward * sensitiveZoomPad / 100;
        }

        if (Input.GetButtonDown("LR"))
            LRUI.SetActive(true);
        if (Input.GetButtonUp("LR"))
            LRUI.SetActive(false);
    }
}