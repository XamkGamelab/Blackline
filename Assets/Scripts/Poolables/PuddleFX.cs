using UnityEngine;

public class PuddleFX : MonoBehaviour, IPoolable
{
    private BasePool<PuddleFX> _pool;

    public void SetPool<T>(BasePool<T> pool) where T : Component, IPoolable
    {
        _pool = pool as BasePool<PuddleFX>;
    }

    public void OnSpawned()
    {
        print("Puddle Spawned");
    }

    public void OnDespawned()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
