using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Minimap : MonoBehaviour
{
    #region Values
    [SerializeField]
    private float mapXsize = 60;    // 맵 전체 X사이즈
    [SerializeField]
    private float mapZsize = 60;    // 맵 전체 Y사이즈
    #endregion

    #region Components
    [SerializeField]
    private ScrollRect scrollRect = null;   // 미니맵 스크롤 컴포넌트
    [SerializeField]
    private GameObject thePlayer = null;    // 플레이어 컴포넌트
    #endregion
    
    void Start()
    {
        scrollRect.normalizedPosition = Vector2.one * 0.5f;
    }

    Vector2 Get2DMapPosition(Vector3 _position)
    {
        Vector3 tmp = _position;

        tmp.x += mapXsize / 2;
        tmp.z += mapZsize / 2;

        float xRatio = tmp.x / mapXsize;
        float zRatio = tmp.z / mapZsize;

        Vector2 minimapPos = Vector2.zero;
        minimapPos.Set(xRatio, zRatio);

        return minimapPos;
    }
    void Update()
    {
        scrollRect.normalizedPosition = Get2DMapPosition(thePlayer.transform.position); // 플레이어의 좌표를 통해 미니맵의 좌표를 업데이트함.
    }
}
