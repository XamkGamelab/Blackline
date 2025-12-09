using UnityEngine;

public class PuddleFX : MonoBehaviour, IPoolable
{
    private BasePool<BulletImpactFX> _pool;

    public void SetPool<T>(BasePool<T> pool) where T : Component, IPoolable
    {
        _pool = pool as BasePool<BulletImpactFX>;
    }

    public void OnSpawned()
    {

    }

    public void OnDespawned()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
