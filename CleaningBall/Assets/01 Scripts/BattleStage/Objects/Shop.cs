using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Values
    [SerializeField]
    private int shopCode = 0;   // 상점을 코드로 구분함.
    [SerializeField]
    private string product = string.Empty;  //  파는 품목
    #endregion

    #region Components
    [SerializeField]
    private Text priceText = null;  // 품목의 가격을 표시하는 텍스트
    #endregion

    #region Properties
    public int ShopCode { get => shopCode; }
    public string Product { get => product; }
    public int Price { get; private set; }
    public int Level { get; private set; }
    #endregion

    private void Start()
    {
        InitializeValue();
    }
    public void InitializeValue()
    {
        Level = DataManager.Instance.GetValueWithName(product);
        Price = Level * 2;
        priceText.text = Price.ToString();
    }

    public bool BuyProduct()
    {
        if(DataManager.Instance.Money < Price)
            return false;

        DataManager.Instance.IncreaseValue(product);
        DataManager.Instance.IncreaseValue("money", -Price);
        InitializeValue();
        return true;
    }
}