using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadCursol : MonoBehaviour
{
    public float padCursolSpeed = 2.0f;     // 動かす速度
    Unit padSelectUnit = null;              // 今掴んでいるユニット
    RaycastHit hitDown;                     // カーソルの下を取得
    Hex Hex;                                // 今下にあるマス

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.bMenuDisplay())    // 何かメニューを表示しているかどうか
        {
            float moveX = Input.GetAxis("Horizontal") * padCursolSpeed;
            float moveY = Input.GetAxis("Vertical") * padCursolSpeed;
            transform.localPosition += new Vector3(moveX, 0.0f, moveY);

            // カーソルの下にあるマスを取得
            int mask = 1 << 6;  // Rayがマスにしか当たらないように設定
            if (Physics.Raycast(transform.position, Vector3.down, out hitDown, 2.0f, mask))
            {
                Hex hitHex = hitDown.transform.GetComponent<Hex>();

                if (Hex != hitHex)
                    Hex = hitHex;

                Hex.SetCursol(true);

                // 掴んでいるユニットを動かす
                if (padSelectUnit)
                    padSelectUnit.PadDrag(hitDown);
            }

            // ユニットを掴む、置く
            if (Input.GetButtonDown("Fire1"))
            {
                if (padSelectUnit == null)　// ユニットを掴んでいない時
                {
                    if (Hex.bUnit)
                        padSelectUnit = Hex.Unit;
                }
                else // ユニットを掴んでいる時
                {
                    padSelectUnit.MouseUp();
                    padSelectUnit = null;
                }
            }
            // 置いたまま素材を獲得
            if (Input.GetButtonDown("Fire3"))
            {
                if(Hex.bUnit)
                    Hex.Unit.ActMaterial();
            }
        }
    }
}
