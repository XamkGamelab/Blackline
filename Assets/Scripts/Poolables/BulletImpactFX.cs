using UnityEngine;

public class BulletImpactFX : MonoBehaviour, IPoolable
{
    [SerializeField]
    protected ParticleSystem _particleSystem;
    
    protected BasePool<BulletImpactFX> _impactFXPool;

    public void SetPool<T>(BasePool<T> pool) where T : Component, IPoolable
    {
        _impactFXPool = pool as BasePool<BulletImpactFX>;
    }

    public virtual void OnSpawned()
    {

    }

    public virtual void OnParticleSystemStopped()
    {
        _impactFXPool.Despawn(this);
    }

    public virtual void OnDespawned()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}

public enum PuddleType
{
    None,
    Android,
    Flesh
}
