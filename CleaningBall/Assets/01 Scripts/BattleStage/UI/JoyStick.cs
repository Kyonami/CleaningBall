﻿using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour {

    #region Values
    [SerializeField]
    private Vector3 stickFirstPos = Vector3.zero;   // 조이스틱 처음 위치
    [SerializeField]
    private Vector3 joyVec = Vector3.zero;          // 조이스틱 중앙부터 스틱까지의 벡터
    
    [SerializeField]
    private float radius = 0f;                      // 반지름
    [SerializeField]
    private float angle = 0f;                       // 중앙 스틱이 만들어낸 각도 (0 ~ 360도)
    [SerializeField]
    private bool isDrag = false;                    // 드래그 중인가
    #endregion

    #region Components
    [SerializeField]
    private Transform plate = null;                 // 조이스틱 판
    [SerializeField]
    private Transform stick = null;                 // 내부 스틱
    #endregion

    #region Properties
    public bool IsDrag { get { return isDrag; } }
    public Vector3 JoyStickPosition { get { return joyVec; } }  // 얘를 대입해서 움직이도록 한다.
    public float Angle { get { return angle; } }
    #endregion

    void Start()
    {
        isDrag = false;
        radius *= Screen.width / 960f;
        stickFirstPos = plate.transform.position;
    }

    // 첫 터치시 실행
    public void PointerDown(BaseEventData _data)
    {
        isDrag = true;
        Drag(_data);
        RotationAngleInDegrees();
    }
    // 터치하고 끄는 동안 실행
    public void Drag(BaseEventData _data)
    {
        PointerEventData touchData = _data as PointerEventData;
        Vector3 pos = touchData.position;   // 터치한 부분의 좌표
        
        joyVec = (pos - stickFirstPos).normalized;  // 터치한 부분의 좌표 - 조이스틱 처음좌표의 정규화 값.
        RotationAngleInDegrees();
        
        stick.position = stickFirstPos + joyVec * radius;   // 조이스틱을 손가락 위치로 옮김.
    }
    // 터치 뗐을 때 실행
    public void PointerUp()
    {
        isDrag = false;
        stick.position = stickFirstPos;
        joyVec = Vector3.zero;
    }

    // 각도 계산하는 함수
    private void RotationAngleInDegrees()
    {
        float theta = Mathf.Atan2(stick.transform.position.y - plate.transform.position.y, stick.transform.position.x - plate.transform.position.x);    
        
        theta -= Mathf.PI / 2.0f;
        
        float tempAngle = (theta * Mathf.Rad2Deg);

        if (tempAngle < 0) tempAngle += 360;

        tempAngle -= 360;

        angle = Mathf.Abs(tempAngle);  // 조이스틱의 중심을 축으로 한뒤, 위 방향을 기준으로 내가 터치한 부분 각도를 계산하는 과정.
    }
}
