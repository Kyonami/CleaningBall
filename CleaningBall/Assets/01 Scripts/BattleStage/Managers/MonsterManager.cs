using System.Collections.Generic;
using UnityEngine;
using FsmDefine;

public class MonsterManager : Singleton<MonsterManager>
{
    #region Values
    [SerializeField]
    private int nMobCount = 10; // 생성될 몬스터 마리 수
    #endregion

    #region Components
    [SerializeField]
    private List<Monster> monsterList = new List<Monster>();    // 생성된 몬스터 리스트
    [SerializeField]
    private List<Monster> attackingMonsterList = new List<Monster>();   // 공격중인 몬스터 리스트
    [SerializeField]
    private Character thePlayer = null; // 플레이어
    GameObject mobParent = null;        // 몬스터를 담아둘 부모 게임 오브젝트
    #endregion

    #region Properties
    public List<Monster> MonsterList { get => monsterList; }
    public List<Monster> AttackingMonsterList { get => attackingMonsterList; }
    public Character ThePlayer { get => thePlayer; }
    #endregion

    private void Start()
    {
        if (nMobCount <= 0)
            return;

        // Create parent
        if(mobParent == null) mobParent = new GameObject();
        mobParent.name = "NPC_Mobs";

        for (int i = 0; i< nMobCount; i++)
        {
            MonsterFSM temp = new MonsterFSM();

            temp.AddStateTransition(EState.STATE_IDLE, EEvent.EVENT_PATROL, EState.STATE_PATROL);
            temp.AddStateTransition(EState.STATE_IDLE, EEvent.EVENT_FINDTARGET, EState.STATE_ATTACK);
            temp.AddStateTransition(EState.STATE_IDLE, EEvent.EVENT_DEAD, EState.STATE_DEAD);

            temp.AddStateTransition(EState.STATE_PATROL, EEvent.EVENT_FINDTARGET, EState.STATE_ATTACK);
            temp.AddStateTransition(EState.STATE_PATROL, EEvent.EVENT_STOPWALK, EState.STATE_IDLE);
            temp.AddStateTransition(EState.STATE_PATROL, EEvent.EVENT_DEAD, EState.STATE_DEAD);

            temp.AddStateTransition(EState.STATE_ATTACK, EEvent.EVENT_LOSTTARGET, EState.STATE_IDLE);
            temp.AddStateTransition(EState.STATE_ATTACK, EEvent.EVENT_STOPWALK, EState.STATE_IDLE);
            temp.AddStateTransition(EState.STATE_ATTACK, EEvent.EVENT_DEAD, EState.STATE_DEAD);

            temp.AddStateTransition(EState.STATE_DEAD, EEvent.EVENT_REVIVE, EState.STATE_IDLE);

            temp.SetCurrentState(EState.STATE_PATROL);
            // 몬스터 FSM을 초기화 하는 작업

            // 몬스터 생성.
            GameObject tempObject = Instantiate(ResourceManager.Instance.monster);
            tempObject.name = "Mob_" + i.ToString();
            Monster mobScript = tempObject.AddComponent<Monster>();

            tempObject.transform.SetParent(mobParent.transform);
            
            // 몬스터에게 영혼을 부여
            mobScript.InitMonsterValue(temp, 5, 3, 7f, DataManager.Instance.MonsterLevel, thePlayer);
            mobScript.InitMonsterFunc();

            monsterList.Add(mobScript);
        }
    }
}
