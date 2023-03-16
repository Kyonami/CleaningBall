using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    #region Values
    private int movingSlot = -1;    // 움직이고 있는 슬롯의 번호
    #endregion

    #region Components
    [SerializeField]
    private Image moveIcon = null;  // 움직일때 시각적으로 띄워줄 아이콘
    public List<Slot> slotList = null;  // 슬롯들 리스트
    #endregion


    public void OnPointerDown(PointerEventData eventData)   // 슬롯 드래그 시작
    {
        moveIcon.rectTransform.position = eventData.position;
        moveIcon.gameObject.SetActive(true);

        Vector2 uiPos = eventData.position;

        for(int i = 0; i < slotList.Count; i++) // 클릭시 클릭한 좌표가 어떤 슬롯의 렉트와 겹치는지 비교.
        {
            if (slotList[i].MyRect.Contains(uiPos) && slotList[i].isAbleToMove)
            {
                movingSlot = i;
                
                string sprName = slotList[i].StrIcon;
                moveIcon.sprite = ResourceManager.Instance.LoadTurretSprite(sprName);

                break;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moveIcon.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveIcon.rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)   // 드래그 종료
    {
        Vector2 uiPos = eventData.position;

        for (int i = 0; i < slotList.Count; i++)    // 내려놓았을 때의 마우스 좌표와 어떤 슬롯이 겹치는지 확인
        {
            if (slotList[i].MyRect.Contains(uiPos) && !slotList[i].isAbleToMove)
            {
                string moveSpr = slotList[movingSlot].Icon.sprite.name;

                slotList[i].Icon.sprite = ResourceManager.Instance.LoadTurretSprite(moveSpr);
                slotList[i].ItemCode = slotList[movingSlot].ItemCode;
            }
        }
        moveIcon.sprite = null;

        SendMessage("DoAfterSlotDrag");
    }
}
