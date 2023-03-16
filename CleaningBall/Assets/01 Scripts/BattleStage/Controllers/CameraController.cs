using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Components
    [SerializeField]
    private Transform targetTr; // 카메라가 쫒아갈 트랜스폼
    #endregion

    private void Start()
    {
        targetTr = GameObject.Find("Player/Head/CameraPivot").transform;
    }
    private void LateUpdate()
    {
        transform.localPosition = targetTr.transform.position;
    }
}
