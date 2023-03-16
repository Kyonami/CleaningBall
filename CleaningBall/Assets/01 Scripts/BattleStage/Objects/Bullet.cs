using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour{

    #region Values
    private Vector3 startPos;   // 총알 출발 좌표
    [SerializeField]
    private float speed = 20f;  // 총알 날아가는 속도
    private int damage;         // 총알 데미지
    #endregion

    #region Components
    [SerializeField]
    private Character targetObj;    // 유도당하는 객체
    #endregion

    public void StartShooting(Character _targetChar, int _damage)
    {
        speed = 20f;
        startPos = transform.position;
        targetObj = _targetChar;
        damage = _damage;

        StartChasingTarget();

        return;
    }

    private void StartChasingTarget()
    {
        if (startPos == null || targetObj == null)
            return;
        
        StartCoroutine(StartBulletSequence());
    }

    private IEnumerator StartBulletSequence()
    {
        yield return StartCoroutine(ChaseTarget()); // 대상 쫒아가기 시작. 쫒아가는게 끝날 때 까지 코루틴이 멈춰있음.
        targetObj.Damage(damage);
        DestroyBullet();
    }

    private IEnumerator ChaseTarget()
    {
        Vector3 temp;

        while (CalculateDistance() >= 1.0f)
        {
            temp = targetObj.transform.position - transform.position;
            transform.Translate(temp.normalized * Time.deltaTime * speed);
            yield return null;
        }
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
    private float CalculateDistance()
    {
        return Vector3.Distance(transform.position, targetObj.transform.position);
    }
}
