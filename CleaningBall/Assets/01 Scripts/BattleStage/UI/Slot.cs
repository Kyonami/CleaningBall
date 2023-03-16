using UnityEngine;
using UnityEngine.UI;

// 각 슬롯들.
public class Slot : MonoBehaviour
{
    #region Values
    [SerializeField]
    private int itemCode = 0;    // 아이템 코드 (터렛 코드)
    private string strIcon = string.Empty;  // 아이콘 이름
    #endregion

    #region Components
    [SerializeField]
    private RectTransform myRTTransform = null; // 내 슬롯 렉트 트랜스폼
    [SerializeField]
    private Rect myRect = Rect.zero; // 내 슬롯 렉트
    [SerializeField]
    private Image icon = null;   // 아이콘
    #endregion

    #region Properties
    public Rect MyRect { get => myRect; }
    public int ItemCode { get => itemCode; set => itemCode = value; }
    public Image Icon { get => icon; }
    #endregion

    public string StrIcon
    {
        get
        {
            if (icon.sprite != null)
                return icon.sprite.name;
            return null;
        }
    }
    public bool isAbleToMove = true;

    private void Awake()
    {
        icon = GetComponent<Image>();
        myRTTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        myRect.x = myRTTransform.position.x - myRTTransform.rect.width / 2;
        myRect.y = myRTTransform.position.y - myRTTransform.rect.height / 2;

        myRect.max = new Vector2(myRTTransform.rect.width, myRTTransform.rect.height);
        myRect.width = myRTTransform.rect.width;
        myRect.height = myRTTransform.rect.height;
    }// 슬롯의 렉트를 좌하단 앵커 좌표 기준으로 다시 계산해놓는다.
}
