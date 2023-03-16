using UnityEngine;
using FsmDefine;

public class AttackState : MonsterState
{
    private float attackCoolDown;

    public AttackState(Monster _parent, int _aniIndex) : base(_parent, _aniIndex)
    {
        parentObj = _parent;
        state = EState.STATE_ATTACK;
    }

    public override void Enter(Character _target)
    {
        parentObj.MyNavMeshAgent.isStopped = true;
        MonsterManager.Instance.AttackingMonsterList.Add(parentObj);
        HPBarPool.Instance.childClass.ConnectWithTarget(parentObj);
    }

    public override void Handle(Character _target)
    {
        ChaseTarget(_target);
        CalculateDistance(_target.transform.position);
        CheckAttack(_target);
    }

    private void ChaseTarget(Character _target) // 타겟 쫒아가기.
    {
        if (Vector3.Distance(parentObj.transform.position, _target.transform.position) > 0.8f)  // 0.8거리 까지 쫒아감.
            parentObj.transform.position = Vector3.MoveTowards(parentObj.transform.position, _target.transform.position, Time.deltaTime * parentObj.Speed);

        // 회전
        Vector3 targetDir = _target.transform.position - parentObj.transform.position;
        float step = 10.0f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(parentObj.transform.forward, targetDir, step, 0.0f);
        parentObj.transform.rotation = Quaternion.LookRotation(newDir); // 타겟을 쳐다봄.
    }
    private void CalculateDistance(Vector3 _target)   // 타겟과의 거리 계산
    {
        if (Vector3.Distance(parentObj.transform.position, _target) >= 10.0f)
        {
            parentObj.SetTransition(EEvent.EVENT_LOSTTARGET);
        }
    }
    private void CheckAttack(Character _target) 
    {
        attackCoolDown -= Time.deltaTime;
        if(Vector3.Distance(parentObj.transform.position, _target.transform.position) < 1f && attackCoolDown <= 0) // 사거리에 들어오고, 쿨타임이 됬으면 공격.
        {
            attackCoolDown = parentObj.AttackCoolTime;
            Attack(_target);
        }
    }
    private void Attack(Character _target)
    {
        _target.Damage(parentObj.MyStat.AttackLevel * parentObj.DamageRatio);
    }
}
