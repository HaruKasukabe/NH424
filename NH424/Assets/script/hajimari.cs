using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hajimari : MonoBehaviour
{
    public Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        fade.FadeIn(2.0f, () => { SceneManager.LoadScene("NewScene"); });
    }
}