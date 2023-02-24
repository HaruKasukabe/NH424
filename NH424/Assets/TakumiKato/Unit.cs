using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int nMoveLong;
    CapsuleCollider col;
    RaycastHit hit;
    RaycastHit oldHit;
    Vector3 oldHexa;
    bool b;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        b = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDrag()
    {
        col.enabled = false;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (!b)
            {
                oldHit = hit;
                oldHexa = transform.position;
                b = true;
            }

            hit.collider.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            oldHit.collider.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");

            transform.position = new Vector3(hit.transform.position.x, 0.65f, hit.transform.position.z);
            hit.collider.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(100.0f, 100.0f, 100.0f));

            if (oldHexa != transform.position)
            {
                oldHit.collider.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));
                oldHit = hit;
                oldHexa = transform.position;
            }
        }
    }

    void OnMouseUp()
    {
        col.enabled = true;
        hit.collider.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));
    }
}
