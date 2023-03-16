using UnityEngine;

/// <summary>
/// 키보드 입력을 통한 캐릭터 움직임 구현
/// </summary>
public class PlayerMove : MonoBehaviour {

    #region Values
    [SerializeField]
    private int mySpeed = 0;    //  속도

    private Vector3 theJoyStickPosBowl = Vector3.zero;      // 중앙 스틱 좌표
    private Vector3 theJoyStickAngleBowl = Vector3.zero;    // 중앙 스틱을 통해 생기는 각의 각도
    #endregion

    #region Components
    [SerializeField]
    private JoyStick theJoyStick = null;    // 조이스틱
    [SerializeField]
    private Animator myAnimator = null;     // 애니메이터
    #endregion

    private void Update()
    {
        Move();
        RotateObject();
        UpdateMoveAnimation();
    }
    private void UpdateMoveAnimation()
    {
        myAnimator.SetBool("Move", theJoyStick.IsDrag);
    }
    private void Move()
    {
        // 조이스틱의 벡터를 가져와서 대상의 좌표에 대입
        theJoyStickPosBowl.Set(theJoyStick.JoyStickPosition.x, 0, theJoyStick.JoyStickPosition.y);
        transform.position += theJoyStickPosBowl * Time.deltaTime * mySpeed;
    }
    private void RotateObject()
    {
        // 대상을 조이스틱 각도만큼 돌림.
        theJoyStickAngleBowl.Set(0, theJoyStick.Angle, 0);
        transform.eulerAngles = theJoyStickAngleBowl;
    }
}
