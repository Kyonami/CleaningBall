using UnityEngine;
using FsmDefine;

public class IdleState : MonsterState
{
    #region Values
    private float sumTime = 0;      // 멍때리는 시간
    private int standDuration = 0;  // 멍 때릴 시간
    #endregion

    public IdleState(Monster _parent, int _aniIndex) : base(_parent, _aniIndex)
    {
        state = EState.STATE_IDLE;
        standDuration = Random.Range(1, 4);
    }

    public override void Enter(Character _target)
    {
        parentObj.MyNavMeshAgent.isStopped = true;
        MonsterManager.Instance.AttackingMonsterList.Remove(parentObj);
        HPBarPool.Instance.childClass.DisconnectWithTarget(parentObj);
    }
    public override void Handle(Character _target)
    {
        CheckTargetIsNear();
    }
    
    private void CheckTargetIsNear()
    {
        if (parentObj.CheckTargetIsInRange())   // 사거리에 있으면 쫒아가거나
            FindTarget();
        else
            Patrol();
    }
    private void FindTarget()
    {
        parentObj.SetTransition(EEvent.EVENT_FINDTARGET);
        sumTime = 0;   
    }

    private void Patrol()
    {
        sumTime += Time.deltaTime;
        if (sumTime >= standDuration)
        {
            parentObj.SetTransition(EEvent.EVENT_PATROL);
            sumTime = 0f;
        }
    }
}
