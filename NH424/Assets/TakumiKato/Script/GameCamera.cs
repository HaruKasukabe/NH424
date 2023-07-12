//=============================================================================
//
// ゲームカメラ クラス [GameCamera.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Camera cam;     // カメラ

    public float sensitiveMoveMouse = 0.8f;     // マウスでのカメラ移動速度
    public float sensitiveZoomMouse = 10.0f;    // マウスでのカメラ拡縮速度

    public float sensitiveMovePad = 0.3f;       // パッドでのカメラ移動速度
    public float sensitiveZoomPad = 3;          // パッドでのカメラ拡縮速度

    [SerializeField] GameObject LRUI; // LRを押した時にでるUIのゲームオブジェクト

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.bMenuDisplay())    // 何かメニューを表示しているかどうか
        {
            // マウスでカメラ移動
            if (Input.GetMouseButton(1))    // 右クリックを押しながら
            {
                float moveX = Input.GetAxis("Mouse X") * sensitiveMoveMouse;
                float moveY = Input.GetAxis("Mouse Y") * sensitiveMoveMouse;
                //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                cam.transform.localPosition -= new Vector3(moveX, 0.0f, moveY);
            }

            // パッド(右スティック)でカメラ移動
            float moveXSti = Input.GetAxis("R_Stick_H") * sensitiveMovePad;
            float moveYSti = Input.GetAxis("R_Stick_V") * sensitiveMovePad;
            //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            cam.transform.localPosition += new Vector3(moveXSti, 0.0f, moveYSti);

            // マウスホイールでカメラ拡縮
            float moveZ = Input.GetAxis("Mouse ScrollWheel") * sensitiveZoomMouse;
            cam.transform.position += cam.transform.forward * moveZ;

            // パッド(LT, RT)でカメラ拡縮
            float trigger = Input.GetAxis("LRTrigger");
            if (trigger > 0)        // LTで拡大
                cam.transform.position += cam.transform.forward * sensitiveZoomPad / 100;
            else if (trigger < 0)   // RTで拡縮
                cam.transform.position -= cam.transform.forward * sensitiveZoomPad / 100;
        }

        if (Input.GetButtonDown("LR"))  // LRを押したときにUI表示
            LRUI.SetActive(true);
        if (Input.GetButtonUp("LR"))    // LRを放したときにUIを消す
            LRUI.SetActive(false);
    }
}