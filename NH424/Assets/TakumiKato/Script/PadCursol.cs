using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadCursol : MonoBehaviour
{
    public float padCursolSpeed = 0.1f;
    Unit padSelectUnit = null;
    RaycastHit hitDown;
    Hex Hex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.bMenuDisplay())
        {
            float moveX = Input.GetAxis("Horizontal") * padCursolSpeed;
            float moveY = Input.GetAxis("Vertical") * padCursolSpeed;
            transform.localPosition += new Vector3(moveX, 0.0f, moveY);


            int mask = 1 << 6;
            if (Physics.Raycast(transform.position, Vector3.down, out hitDown, 2.0f, mask))
            {
                Hex hitHex = hitDown.transform.GetComponent<Hex>();

                if (Hex != hitHex)
                    Hex = hitHex;

                Hex.SetCursol(true);

                if (padSelectUnit)
                    padSelectUnit.PadDrag(hitDown);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (padSelectUnit == null)
                {
                    if (Hex.bUnit)
                        padSelectUnit = Hex.Unit;
                }
                else
                {
                    padSelectUnit.MouseUp();
                    padSelectUnit = null;
                }
            }
        }
    }
}
