using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SEASON
{
    SPRING,
    SUMMER,
    FALL,
    WINTER
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public SEASON season = SEASON.SPRING;
    public float fOneSeasonTime;
    public int level = 0;
    public float wood = 0.0f;
    public float food = 0.0f;
    public float stone = 0.0f;
    public float iron = 0.0f;

    float Hex_Width = 1.1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            level++;
    }

    public Vector3 Cal_HexPosToViewLocalPos(Vector3 hexPos)
    {
        // Yï˚å¸çÇÇ≥
        float Hex_Height = Hex_Width * Mathf.Sin(60.0f * Mathf.Deg2Rad);

        // Xï˚å¸ÇÃÇ∏ÇÍ
        float Hex_Adjust = Hex_Width * Mathf.Cos(60.0f * Mathf.Deg2Rad);

        float grid_X = Hex_Width * hexPos.x + Hex_Adjust * Mathf.Abs(hexPos.z % 2);
        float grid_Z = Hex_Height * hexPos.z;

        return new Vector3(grid_X, 0.0f, grid_Z);
    }
}
