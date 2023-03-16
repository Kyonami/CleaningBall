using UnityEngine;

public class Player : Character
{
    #region Components
    [SerializeField]
    private Turret[] myTurrets = null;
    [SerializeField]
    private GameObject theGameOverUI = null;
    #endregion

    private void Start()
    {
        CharacterStat temp = new CharacterStat(DataManager.Instance.AtkDamageLevel, DataManager.Instance.HPLevel);
        
        MyStat = temp;
        InitializeValue();
    }
    protected override void Dead()
    {
        gameObject.SetActive(false);
        for(int i = 0; i < myTurrets.Length; i++)
        {
            myTurrets[i].gameObject.SetActive(false);
        }
        if (theGameOverUI != null)
            theGameOverUI.SetActive(true);
        DataManager.Instance.OnDead();
    }
}
