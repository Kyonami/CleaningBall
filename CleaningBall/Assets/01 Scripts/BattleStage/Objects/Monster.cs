using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FsmDefine;

public class Monster : Character
{
    // About Stat
    #region Values
    [SerializeField]
    private int sightRadius;
    [SerializeField]
    private int rewardMoney;
    [SerializeField]
    private int damageRatio;
    [SerializeField]
    private float speed;
    [SerializeField]
    private readonly float attackCoolTime = 0.5f;
    [SerializeField]
    private Vector3 startPos = Vector3.zero;
    private Rect rangeRect = new Rect();
    #endregion

    #region Components
    [SerializeField]
    private Animator myAnimator;
    [SerializeField]
    public GameObject myModel;
    private Character theTarget;
    [SerializeField]
    private NavMeshAgent myNavMeshAgent;

    private MonsterFSM myFSM;
    private List<MonsterState> stateList = new List<MonsterState>();
    private MonsterState currentState;
    #endregion

    #region Properties
    public int SightRadius { get => sightRadius; }
    public int RewardMoney { get => rewardMoney; }
    public int DamageRatio { get => damageRatio; }
    public float Speed { get => speed; }
    public float AttackCoolTime { get => attackCoolTime; }
    public NavMeshAgent MyNavMeshAgent { get => myNavMeshAgent; }
    #endregion

    public void Update()
    {
        currentState.Handle(theTarget);
    }
    public void InitMonsterValue(MonsterFSM _fsm, int _sightRadius, int _damageRatio, float _speed, int _rewardMoney, Character _target)
    {
        if (myAnimator == null)
            myAnimator = transform.GetChild(0).GetComponent<Animator>();
        if (myModel == null)
            myModel = transform.GetChild(0).gameObject;
        if (myNavMeshAgent == null)
            myNavMeshAgent = GetComponent<NavMeshAgent>();

        CharacterStat temp = new CharacterStat(0, 1);
        MyStat = temp;

        //Give Spirit
        myFSM = _fsm;
        sightRadius = _sightRadius;
        damageRatio = _damageRatio;
        speed = _speed;
        rewardMoney = _rewardMoney;
        theTarget = _target;
        MyKind = ECharacterKind.MONSTER;

        //State List
        stateList.Add(new IdleState(this, 0));
        stateList.Add(new PatrolState(this, 1));
        stateList.Add(new AttackState(this, 2));
        stateList.Add(new DeadState(this, 3));
    }
    public void InitMonsterFunc()
    {
        EState curStateID = myFSM.GetCurrentStateId();
        currentState = stateList.Find(o => o.state == curStateID);  // 현재 상태 다시 로딩.

        // Initialize position
        float xRandom = Random.Range(-55, 55);
        float zRandom = Random.Range(-55, 55);
        transform.position = new Vector3(xRandom, 0, zRandom);
        startPos.Set(xRandom, 0, zRandom);  // 리스폰.

        InitializeValue();  // HP등을 초기화
        myModel.gameObject.SetActive(true);
    }
    public void SetTransition(EEvent _inputEventId)
    {
        myFSM.StateTransition(_inputEventId);
        EState curId = myFSM.GetCurrentStateId();

        currentState = stateList.Find(o => o.state == curId);
        
        currentState.Enter(theTarget);
        myAnimator.SetInteger("State", currentState.aniIndex);
    }

    public bool CheckTargetIsInRange()
    {
        rangeRect.Set(transform.position.x - sightRadius, // x
                transform.position.z - sightRadius, // y
                sightRadius * 2, // width
                sightRadius * 2  // height
                );

        // When a point comes in to another area
        Vector2 targetPos = Vector2.zero;
        targetPos.Set(theTarget.transform.position.x, theTarget.transform.position.z);

        if (rangeRect.Contains(targetPos) == true)
            return true;
        else
            return false;
    }

    protected override void Dead()
    {
        SetTransition(EEvent.EVENT_DEAD);
        myModel.gameObject.SetActive(false);
    }
}