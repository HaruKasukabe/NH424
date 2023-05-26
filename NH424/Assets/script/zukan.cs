using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zukan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Zukan()
    {
        Invoke("Zukan", 1.5f);
        SceneManager.LoadScene(""); //ê}ä”ÇäJÇ≠
    }
}
