using UnityEngine;
using Zenject;

public class BulletImpactFX : MonoBehaviour, IPoolable
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    
    private BasePool<BulletImpactFX> _impactFXPool;

    [SerializeField]
    private PuddleType _puddleType;
    [SerializeField]
    private bool _puddleOnCollision;
    [SerializeField]
    [Range(0f, 1f)]
    private float _puddleRatio;

    //[Inject(Id = _puddleType)]
    private BasePool<PuddleFX> _puddleFXPool;

    [Inject]
    private void Construct(PuddleFX _puddleFXPool)
    {

    }

    public void SetPool<T>(BasePool<T> pool) where T : Component, IPoolable
    {
        _impactFXPool = pool as BasePool<BulletImpactFX>;
    }

    public void OnSpawned()
    {

    }

    public void OnParticleCollision(GameObject other)
    {
        
    }

    public void OnParticleSystemStopped()
    {
        _impactFXPool.Despawn(this);
    }

    public void OnDespawned()
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
