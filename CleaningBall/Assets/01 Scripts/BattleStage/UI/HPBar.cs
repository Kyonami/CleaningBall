using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    #region Values
    Vector3 gapSize = new Vector3(0, 0, 1.2f);  // 대상의 머리 위.
    #endregion

    #region Components
    [SerializeField]
    protected Character theTarget = null;     // 대상 컴포넌트
    [SerializeField]
    protected Image myImage = null;       // 내 HP바 이미지
    #endregion

    public Character TheTarget { get { return theTarget; } }

    public void MakeConnection(Character _target)   // 타겟과 연동
    {
        theTarget = _target;
        UpdatePosition();
        gameObject.SetActive(true);
    }

    public void StopConnection()    // 연동 해제
    {
        theTarget = null;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdatePosition();
        UpdateGauge();
    }

    private void UpdatePosition()   // 타겟 머리위를 계속 쫒아다니게 함.
    {
        if (theTarget == null)
            return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(theTarget.transform.position + gapSize);

        float x = screenPos.x;

        myImage.transform.position = new Vector3(x, screenPos.y, myImage.transform.position.z);
    }

    public void UpdateGauge()
    {
        myImage.fillAmount = theTarget.GetCurrentHPRatio();
    }
}
