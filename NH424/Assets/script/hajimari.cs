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
        ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Start);
        Invoke("StartGame", 2.5f);
        fade.FadeIn(2.0f, () => { SceneManager.LoadScene("NewScene"); });
    }
}
