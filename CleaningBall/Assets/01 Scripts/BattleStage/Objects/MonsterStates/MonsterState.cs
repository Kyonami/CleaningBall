using FsmDefine;

// 모든 상태들은 MonsterState를 상속 받는다.
public abstract class MonsterState
{
    #region Values
    public int aniIndex;    // 상태마다 각자의 애니메이션 번호
    public EState state;    // 상태 -> 각각의 상태 객체들은 상태 키와 연동됨.
    #endregion
    #region Components
    protected Monster parentObj;    // 부모 객체 -> 상태를 가지고 있는 객체 (몬스터)
    #endregion

    public MonsterState() { }
    public MonsterState(Monster _parent, int _aniIndex)
    {
        parentObj = _parent;
        aniIndex = _aniIndex;
    }
    // 상태에 돌입 했을 때
    public abstract void Enter(Character _target);  
    // 상태에서 해야할 행동 (Update에서 실행)
    public abstract void Handle(Character _target); 
}

