using System.Collections.Generic;
using UnityEngine;

public class HPBarPool : ObjectPool<HPBarPool, HPBar>
{
    #region Components
    [SerializeField]
    private GameObject hpBarParent = null;
    #endregion
    

    private void Start()
    {
        InitPool(this, ResourceManager.Instance.hpBar, "HpBar", 20, hpBarParent);
    }
    public void ConnectWithTarget(Character _target)
    {   // 타겟을 받아서 풀에서 HP바를 하나 뗴서 연결해줌
        GetObject().MakeConnection(_target);
    }
    public void DisconnectWithTarget(Character _target)
    {
        // 타겟을 받아서 그 타겟에 붙은 HP바의 연결을 끊어줌
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].TheTarget == _target)
            {
                objectList[i].StopConnection();
                return;
            }
        }
    }
}
