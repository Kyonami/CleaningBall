using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 모든 돈을 표시하는 텍스트에게 이 컴포넌트를 추가해줍니다.
/// </summary>
public class MoneyText : MonoBehaviour
{
    #region Components
    [SerializeField]
    private Text myMoneyText = null;
    #endregion
    
    public void Awake()
    {
        DataManager.Instance.AddObserver(gameObject);
        UpdateValue();
    }
    public void UpdateValue()
    {
        myMoneyText.text = DataManager.Instance.Money.ToString();
    }
    public void OnDataDelete()
    { 
        UpdateValue();
    }
}
