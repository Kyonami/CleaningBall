using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ResourceManager : Singleton<ResourceManager> {

    #region Values
    [SerializeField]
    private int turretSpriteCount = 0;
    [SerializeField]
    private int turretModelCount = 0;
    #endregion

    #region Components
    public List<Sprite> spriteList = new List<Sprite>();    // 스프라이트 리소스
    public List<TurretModel> turretModelList = new List<TurretModel>();
    public HPBar hpBar = null;
    public GameObject bullet = null;
    public GameObject monster = null;
    #endregion

    private void Awake()    // 최적화를 위해 리소스들을 미리 로드 해놓음
    {
        DontDestroyOnLoad(Instance);
        if (Instance != this)
            Destroy(gameObject);

        StringBuilder tempStr = new StringBuilder();

        hpBar = Resources.Load("UI/HPBar", typeof(HPBar)) as HPBar;
        bullet = Resources.Load("Bullet") as GameObject;
        monster = Resources.Load("Monster") as GameObject;
        for (int i = 0; i < turretSpriteCount; i++)
        {
            tempStr.Append("TurretSprite/");
            tempStr.Append(i.ToString());
            spriteList.Add(Resources.Load(tempStr.ToString(), typeof(Sprite)) as Sprite);
            tempStr.Clear();
        }
        for(int i = 0; i < turretModelCount; i++)
        {
            tempStr.Append("TurretModel/");
            tempStr.Append(i.ToString());
            turretModelList.Add(Resources.Load(tempStr.ToString(), typeof(TurretModel)) as TurretModel);
            tempStr.Clear();
        }
    }

    public Sprite LoadTurretSprite(string _dataName)
    {
        return spriteList.Find(o => o.name == _dataName);
    }

}