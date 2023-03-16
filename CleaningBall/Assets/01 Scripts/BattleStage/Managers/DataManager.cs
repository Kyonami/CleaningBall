using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager>
{
    #region Values
    [SerializeField]
    private int money;  // 플레이어가 저장한 돈
    [SerializeField]
    private int atkDamageLevel; // 공격력 레벨
    [SerializeField]
    private int hpLevel;  // 체력 레벨
    [SerializeField]
    private int monsterLevel;   // 몬스터 레벨
    [SerializeField]
    private int[] turretArray = new int[3]; // 터렛 배치
    #endregion

    #region Components
    [SerializeField]
    private List<GameObject> dataObservers = null;
    #endregion

    #region Properties
    public int Money
    {
        get => money;
        set
        {
            money = value;
            InformToObservers("UpdateValue");
        }
    }
    public int AtkDamageLevel { get => atkDamageLevel; }
    public int HPLevel { get => hpLevel; }
    public int MonsterLevel { get => monsterLevel; }
    public int[] TurretArray { get => turretArray; }
    #endregion

    private void Awake()
    {
        //DontDestroyOnLoad(Instance);
        //if (Instance != this)
        //    Destroy(gameObject);
        LoadData();
    }
    private void Start()
    {
        InformToObservers("UpdateValue");
    }

    private void LoadData()
    {
        atkDamageLevel = 1;
        hpLevel = 1;
        monsterLevel = 1;
        money = 0;
        turretArray[0] = 0;
        turretArray[1] = 0;
        turretArray[2] = 0;

        if (PlayerPrefs.HasKey("atkDamageLevel"))
            atkDamageLevel = PlayerPrefs.GetInt("atkDamageLevel");
        if (PlayerPrefs.HasKey("hpLevel"))
            hpLevel = PlayerPrefs.GetInt("hpLevel");
        if (PlayerPrefs.HasKey("monsterLevel"))
            monsterLevel = PlayerPrefs.GetInt("monsterLevel");
        if (PlayerPrefs.HasKey("money"))
            money = PlayerPrefs.GetInt("money");
        if(PlayerPrefs.HasKey("turretArray_0"))
            turretArray[0] = PlayerPrefs.GetInt("turretArray_0");
        if (PlayerPrefs.HasKey("turretArray_1"))
            turretArray[1] = PlayerPrefs.GetInt("turretArray_1");
        if (PlayerPrefs.HasKey("turretArray_2"))
            turretArray[2] = PlayerPrefs.GetInt("turretArray_2");
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("atkDamageLevel", atkDamageLevel);
        PlayerPrefs.SetInt("hpLevel", hpLevel);
        PlayerPrefs.SetInt("monsterLevel", monsterLevel);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("turretArray_0", turretArray[0]);
        PlayerPrefs.SetInt("turretArray_1", turretArray[1]);
        PlayerPrefs.SetInt("turretArray_2", turretArray[2]);
    }

    public void IncreaseValue(string _kind, int _value = 1)
    {
        switch (_kind)
        {
            case "atkDamageLevel":
                atkDamageLevel += _value;
                break;
            case "hpLevel":
                hpLevel += _value;
                break;
            case "monsterLevel":
                monsterLevel += _value;
                break;
            case "money":
                money += _value;
                break;
        }
        SaveData();
        InformToObservers("UpdateValue");
    }
    public void SetTurretData(int _number, int _kind)
    {
        turretArray[_number] = _kind;
        switch (_number)
        {
            case 0:
                PlayerPrefs.SetInt("turretArray_0", turretArray[0]);
                break;
            case 1:
                PlayerPrefs.SetInt("turretArray_1", turretArray[1]);
                break;
            case 2:
                PlayerPrefs.SetInt("turretArray_2", turretArray[2]);
                break;
        }
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        LoadData();
        SaveData();
        InformToObservers("OnDataDelete");
    }
    public void OnDead()
    {
        money = 0;

        SaveData();
        LoadData();
        InformToObservers("UpdateValue");
    }

    public void AddObserver(GameObject _observer)
    {
        dataObservers.Add(_observer);
    }
    public void ClearObserverList()
    {
        dataObservers.Clear();
    }
    public void InformToObservers(string _methodName)
    {
        for(int i = 0; i < dataObservers.Count; i++)
        {
            dataObservers[i].SendMessage(_methodName);
        }
    }

    public int GetValueWithName(string _valueName)
    {
        switch (_valueName)
        {
            case "atkDamageLevel":
                return atkDamageLevel;
            case "hpLevel":
                return hpLevel;
            case "monsterLevel":
                return monsterLevel;
            case "money":
                return money;
            default:
                return 0;
        }
    }
}
