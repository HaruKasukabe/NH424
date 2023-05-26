using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charaInfo : MonoBehaviour
{
    public static charaInfo instance = null;

    private List<CharacterInfo> CharaInfo = new List<CharacterInfo>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<CharacterInfo> GetInfo()
    {
        return CharaInfo;
    }
}
