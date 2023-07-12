//=============================================================================
//
// UIカーソル クラス [UICursol.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICursol : MonoBehaviour
{
    RectTransform trs;
    GameObject obj;
    [SerializeField] float cursolSpeed = 2.2f;  // カーソルの移動速度

    // Start is called before the first frame update
    void Start()
    {
        trs = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * cursolSpeed;
        float moveY = Input.GetAxis("Vertical") * cursolSpeed;

        trs.position += new Vector3(moveX, moveY, 0.0f) * 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventSystem ev = EventSystem.current;
        ev.SetSelectedGameObject(collision.gameObject);
    }
}
