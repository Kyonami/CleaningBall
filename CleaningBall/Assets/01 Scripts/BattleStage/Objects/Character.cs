using UnityEngine;

/// <summary>
/// 모든 캐릭터 -> 플레이어, 몬스터
/// </summary>
/// 

public enum ECharacterKind
{
    MONSTER,
    PLAYER,
    CREATURE,
    NONE
}

public abstract class Character : MonoBehaviour {

    #region Values
    [SerializeField]
    private int hp = 0;   // 캐릭터 스텟상 HP
    [SerializeField]
    private int currentHp = 0;    // 현재 HP
    [SerializeField]
    private Vector3 hpBarAnchor;
    [SerializeField]
    private ECharacterKind myKind = ECharacterKind.NONE;
    private bool isDead;  // 죽었는가
    #endregion

    #region Components
    [SerializeField]
    private CharacterStat myStat = null;    // 캐릭터 스테이터스 -> 상태를 말하는 것이 아닌 스텟.
    #endregion

    #region Properties
    public CharacterStat MyStat
    {
        get
        {
            return myStat;
        }
        set
        {
            myStat = value;
            this.InitializeHP();
        }
    }
    public ECharacterKind MyKind { get => myKind; set => myKind = value; }
    #endregion
    
    public float GetDistanceFromPos(Transform _targetTransform)
    {
        float x = transform.position.x - _targetTransform.position.x;
        float y = transform.position.z - _targetTransform.position.z;
        
;       return Mathf.Sqrt(x * x + y * y);
    }
    protected void InitializeValue()
    {
        switch (myKind)
        {
            case ECharacterKind.MONSTER:
                myStat.HPLevel = DataManager.Instance.MonsterLevel;
                myStat.AttackLevel = DataManager.Instance.MonsterLevel;
                break;
            case ECharacterKind.CREATURE:
                myStat.HPLevel = 1;
                myStat.AttackLevel = DataManager.Instance.AtkDamageLevel;
                break;
            case ECharacterKind.PLAYER:
                myStat.HPLevel = DataManager.Instance.HPLevel;
                myStat.AttackLevel = 1;
                break;
        }
        this.InitializeHP();
    }
    protected void InitializeHP()
    {
        hp = 20 * MyStat.HPLevel;
        currentHp = hp;
        isDead = false;
    }
    public void Damage(int _damage)
    {
        if (!isDead)
        {
            currentHp -= _damage;

            if (currentHp <= 0)
            {
                isDead = true;
                Dead();
                return;
            }
        }
    }
    public float GetCurrentHPRatio()
    {
        return ((float)currentHp / (float)hp);
    }
    protected abstract void Dead();
}
