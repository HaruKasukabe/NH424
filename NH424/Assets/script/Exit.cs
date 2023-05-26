using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public GameObject Ingame;
    public GameObject End;
    // Start is called before the first frame update
    //InvokeRepeating("EndGame",2.0f);

    // Update is called once per frame
    void Start()
    {
        //Invoke("EndGame", 2.3f);
        Ingame.SetActive(true);
        End.SetActive(false);
    }
    public void MainView()
    {
        Ingame.SetActive(true);
        End.SetActive(false);
    }

    public void SubView()
    {
        Ingame.SetActive(false);
        End.SetActive(true);
    }

    void Update()
    {
        
    }
//    public void EndGame()
//    {
//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
//#else
//    //yield return new WaitForSeconds(1.0f);
//    Application.Quit();//ゲームプレイ終了

//#endif
//    }

}
