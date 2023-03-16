using System.Collections;
using UnityEngine;

/// <summary>
/// Turrets
/// </summary>

public class Turret : Character {

    #region Values
    [SerializeField]
    private readonly int myDamage = 1;  // 내 데미지 -> 총알 한발의 데미지
    [SerializeField]
    private float myDelay = 1f;     // 총알 발사 딜레이
    #endregion

    #region Components
    [SerializeField]
    private Character theTarget = null; // 내가 총을 발사할 타겟
    [SerializeField]
    private TurretModel myModel = null;   // 내 모델 (터렛)
    [SerializeField]
    private TurretMove myTurretMoveInfo = null;     // 꼬리 기능 컴포넌트
    #endregion

    private void Awake()
    {
        CharacterStat temp = new CharacterStat(1, 1);
        MyStat = temp;
    }
    private void Start()
    {
        myModel = GetComponentInChildren<TurretModel>();
        myTurretMoveInfo = GetComponent<TurretMove>();
        StartCoroutine(RepeatShootingSequence());
        InitializeValue();
    }
    private void Update()
    {
        ChoiceTarget();
        RotateTurret();
    }
    private void ChoiceTarget() // 총을 쏠 타겟을 설정함.
    {
        if (MonsterManager.Instance.AttackingMonsterList.Count > 0)
        {
            theTarget = MonsterManager.Instance.AttackingMonsterList[0];
            return;
        }
        theTarget = null;
    }

    private IEnumerator RepeatShootingSequence()    // 딜레이를 두고 쏘는걸 반복함.
    {
        ShootBullet();
        yield return new WaitForSeconds(myDelay);
        StartCoroutine(RepeatShootingSequence());
    }

    public void ShootBullet()
    {
        if (theTarget == null)
            return;

        Bullet temp = BulletPool.Instance.GetObject();
        temp.transform.position = transform.position + myModel.transform.forward * 0.5f;
        temp.StartShooting(theTarget, myDamage * MyStat.AttackLevel);
    }

    private void RotateTurret() // 공격중인 대상이 있으면 대상을 바라보도록 함.
    {
        if (theTarget == null)
        {
            myTurretMoveInfo.LookChasingTarget();
            return;
        }
        Vector3 temp = theTarget.transform.position;
        temp.y = myModel.head.transform.position.y;
        myModel.head.transform.LookAt(temp);
    }

    protected override void Dead()
    {
        gameObject.SetActive(false);
    }
}
