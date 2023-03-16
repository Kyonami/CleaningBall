using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    #region Components
    [SerializeField]
    private Text moneyText = null;  // 남은 돈 텍스트
    [SerializeField]
    private Text alertText = null;  // 잔액이 부족합니다.
    [SerializeField]
    private List<Shop> shopList = new List<Shop>();
    #endregion

    private void Start()
    {
        DataManager.Instance.AddObserver(gameObject);
        UpdateValue();
    }
    private void UpdateValue()
    {
        moneyText.text = DataManager.Instance.Money.ToString();
    }
    public void Buy(int _shopCode)
    {
        Shop temp = shopList.Find(o => o.ShopCode == _shopCode);

        if (!temp.BuyProduct())
        {
            StartCoroutine(ShowAlertText());
        }
    }
    public void OnDataDelete()
    {
        for (int i = 0; i < shopList.Count; i++)
            shopList[i].InitializeValue();
    }

    private IEnumerator ShowAlertText()
    {
        alertText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        alertText.gameObject.SetActive(false);
    }
}