using UnityEngine;

public class BulletPool : ObjectPool<BulletPool, Bullet>
{
    #region Components
    [SerializeField]
    private Bullet bulletSource = null;
    #endregion

    private void Start()
    {
        InitPool(this, bulletSource, "Bullet", 20);
    }
}
