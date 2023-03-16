using UnityEngine;

public class TurretArrangement : MonoBehaviour
{
    #region Components
    [SerializeField]
    private Slot[] theTurretSlot = null;    // 터렛이 들어가는 위 3개 슬롯
    #endregion

    // 터렛 슬롯에 배치된 터렛의 정보를 관리함.

    private void Awake()
    {
        DataManager.Instance.AddObserver(gameObject);
        InitializeData();
        UpdateValue();
    }
    public void DoAfterSlotDrag()
    {
        SetTurretArray();
        UpdateValue();
    }
    public void UpdateValue()
    {
        for (int i = 0; i < theTurretSlot.Length; i++)
        {
            theTurretSlot[i].Icon.sprite = ResourceManager.Instance.LoadTurretSprite(DataManager.Instance.TurretArray[i].ToString());
        }
    }
    public void OnDataDelete()
    {
        InitializeData();
        UpdateValue();
    }
    private void InitializeData()
    {
        for (int i = 0; i < theTurretSlot.Length; i++)
            theTurretSlot[i].ItemCode = DataManager.Instance.TurretArray[i];
    }
    private void SetTurretArray()
    {
        for (int i = 0; i < theTurretSlot.Length; i++)
        {
            DataManager.Instance.SetTurretData(i, theTurretSlot[i].ItemCode);
        }
    }
}
