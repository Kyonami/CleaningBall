using UnityEngine;
using FsmDefine;

public class PatrolState : MonsterState
{
    #region Values
    Vector3 destPos = Vector3.zero; // 목적지 좌표
    #endregion

    public PatrolState(Monster _monster, int _aniIndex) : base(_monster, _aniIndex)
    {
        parentObj = _monster;
        state = EState.STATE_PATROL;
        MakeNewDestPos();
    }

    public override void Enter(Character _target)
    {
        MonsterManager.Instance.AttackingMonsterList.Remove(parentObj);
        parentObj.MyNavMeshAgent.isStopped = false;
        MakeNewDestPos();
        Patrol();
    }
    public override void Handle(Character _target)
    {
        CheckTargetIsInRange();
        CheckArrive();
    }
    
    private void CheckArrive()
    {
        if (parentObj.MyNavMeshAgent.remainingDistance <= parentObj.MyNavMeshAgent.stoppingDistance)
        {
            parentObj.SetTransition(EEvent.EVENT_STOPWALK);
            MakeNewDestPos();
            parentObj.MyNavMeshAgent.SetDestination(destPos);
        }
    }
    private void Patrol()
    {
        // 회전
        Vector3 targetdir = destPos - parentObj.transform.position;
        float step = 10.0f * Time.deltaTime;
        Vector3 newdir = Vector3.RotateTowards(parentObj.transform.forward, targetdir, step, 0.0f);
        parentObj.transform.rotation = Quaternion.LookRotation(newdir);
    }

    private void CheckTargetIsInRange()
    {
        if (parentObj.CheckTargetIsInRange())
        {
            parentObj.MyNavMeshAgent.isStopped = true;
            parentObj.SetTransition(EEvent.EVENT_FINDTARGET);

            parentObj.transform.position = Vector3.MoveTowards(parentObj.transform.position, destPos, Time.deltaTime * parentObj.Speed);
        }
    }

    private void MakeNewDestPos()
    {
        destPos.x = Random.Range(-55, 55);
        destPos.y = 0;
        destPos.z = Random.Range(-55, 55);
    }
}

//CheckArrive
//if (parentObj.transform.position == destPos)
//{
//    parentObj.SetTransition(EEvent.EVENT_STOPWALK);
//}

    //Patrol
//parentObj.transform.position = Vector3.MoveTowards(parentObj.transform.position, destPos, Time.deltaTime * parentObj.Speed);
//parentObj.MyNavMeshAgent.SetDestination(destPos);
