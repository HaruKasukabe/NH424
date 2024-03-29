//=============================================================================
//
// セレクトボタン クラス [SelectButton.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class SelectButtons : MonoBehaviour
{
    public static SelectButtons instance = null;

    [SerializeField] List<Button> buttons;
    [SerializeField] List<bool> keys;
    public TextMeshProUGUI title;
    bool bSelectMenu = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public int Selected
    {
        get
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i])
                    return i;
            }

            return -1;
        }
    }

    public IEnumerator CorGetInput(Unit unit, Unit myUnit)
    {
        //keysをすべてfalseにする
        for (int i = 0; i < keys.Count; i++)
        {
            keys[i] = false;
        }

        //ゲームオブジェクトを表示
        gameObject.SetActive(true);
        GameManager.instance.SetUICursol(true);
        bSelectMenu = false;

        title.text = "仲間にしますか？\n必要食材：" + unit.sta.cost + "\n所持食材：" + GameManager.instance.food.ToString("f0");

        //いずれかのボタンが押されるまで待機
        yield return new WaitWhile(() => Selected == -1);

        // 仲間にする選択をした時
        if (Selected == 0)
            if (unit.BeMyFriend())
            {
                myUnit.score.friendNum++;
                if (myUnit.bTags[(int)UnitTag.秘訣は笑顔])
                    myUnit.actNum = myUnit.sta.moveNum;
            }

        //ゲームオブジェクトを非表示
        gameObject.SetActive(false);
        GameManager.instance.SetUICursol(false);
        bSelectMenu = true;
    }

    // 各ボタンに処理を割り当て
    void Start()
    {
        //keysをbuttonsと同じ長さに
        keys = Enumerable.Repeat(false, buttons.Count).ToList();


        //各ボタンに処理を割り当てる
        for (int i = 0; i < buttons.Count; i++)
        {
            //一旦別の変数に格納しないとエラー発生
            int a = i;

            //buttonを押すと該当するkeyをtrueに
            buttons[a].onClick.AddListener(() => keys[a] = true);
        }

        //オブジェクトを非表示にする
        gameObject.SetActive(false);
    }
    public bool GetbFriendSelect()
    {
        return bSelectMenu;
    }
}