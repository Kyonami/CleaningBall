using UnityEngine;
using FsmDefine;

public class DeadState : MonsterState
{
    #region Values
    float sumTime = 0.0f;   // 죽었을때 쿨타임을 돌리기 위한 변수
    #endregion

    public DeadState(Monster _parent, int _aniIndex) : base(_parent, _aniIndex)
    {
        parentObj = _parent;
        state = EState.STATE_DEAD;
    }

    public override void Enter(Character _target)
    {
        parentObj.MyNavMeshAgent.isStopped = true;
        MonsterManager.Instance.AttackingMonsterList.Remove(parentObj);
        HPBarPool.Instance.childClass.DisconnectWithTarget(parentObj);
        DataManager.Instance.Money += parentObj.RewardMoney;
        
        sumTime = 0.0f;
    }

    public override void Handle(Character _target)
    {
        sumTime += Time.deltaTime;

        if (sumTime <= 3.0f)
            return;

        Revive();
    }

    private void Revive()
    {
        parentObj.InitMonsterFunc();
        parentObj.SetTransition(EEvent.EVENT_REVIVE);
    }
}
