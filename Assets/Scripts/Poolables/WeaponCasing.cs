using UnityEngine;

public class WeaponCasing : MonoBehaviour, IPoolable
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    private BasePool<WeaponCasing> _pool;

    public void SetPool<T>(BasePool<T> pool) where T : Component, IPoolable
    {
        _pool = pool as BasePool<WeaponCasing>;
    }

    public void OnSpawned()
    {

    }

    public void OnParticleSystemStopped()
    {
        _pool.Despawn(this);
    }

    public void OnDespawned()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
