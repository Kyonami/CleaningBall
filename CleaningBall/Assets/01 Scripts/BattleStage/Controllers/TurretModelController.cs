using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretModelController : MonoBehaviour
{
    #region Components
    [SerializeField]
    private Transform[] theTurretParent = null;    // tail1, tail2, tail3
    #endregion

    private void Awake()
    {
        ApplyTurretModel();
    }
    private void ApplyTurretModel()
    {   // 게임이 시작되면 터렛 모델을 생성 해줌.
        for (int i = 0; i < theTurretParent.Length; i++) {
            Instantiate(ResourceManager.Instance.turretModelList[DataManager.Instance.TurretArray[i]], theTurretParent[i]);
        }
    }
}
