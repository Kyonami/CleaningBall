using UnityEngine;
using System.Collections;

public class TurretMove : MonoBehaviour
{
    #region Values
    [SerializeField]
    private float speed = 0.0f;     // 속도
    [SerializeField]
    private float distance = 2.0f;  // 앞 꼬리와의 거리
    private Vector3 dir;            // 현재 내가 가야할/가고있는 방향
    #endregion

    #region Components
    [SerializeField]
    private GameObject chasingTarget = null;  // 내 앞의 게임 오브젝트 등록
    [SerializeField]
    private TurretModel turretModel = null;     // TurretMove 역할을 하는 게임의 모델
    #endregion

    private void Awake()
    {
        if (chasingTarget == null)
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        turretModel = GetComponentInChildren<TurretModel>();    // 터렛 모델 인식
        StartCoroutine(StartChaseTarget());
    }
    private IEnumerator StartChaseTarget()
    {
        yield return new WaitUntil(() => CalculateDistance() >= distance);  // 쫒아가는 대상과 일정 거리가 벌어지면 코루틴 실행

        dir = chasingTarget.transform.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * speed);   // 앞 대상을 향하는 벡터 대로 이동

        StartCoroutine(StartChaseTarget()); // 코루틴 반복
    }
    private float CalculateDistance()
    {
        return Vector3.Distance(transform.position, chasingTarget.transform.position);  // 쫒아가는 대상과의 거리 반환
    }
    public void LookChasingTarget() // 쫒아가는 대상을 바라보게 함.
    {
        Vector3 temp = chasingTarget.transform.position;
        temp.y = turretModel.transform.position.y;
        turretModel.plate.transform.LookAt(temp);
        turretModel.head.transform.LookAt(temp);
    }
} 
