using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AndroidImpactFX : BulletImpactFX
{
    [SerializeField]
    private bool _puddleOnCollision;
    [SerializeField]
    [Range(0f, 1f)]
    private float _puddleRatio;

    private List<ParticleCollisionEvent> _collisionEvents = new List<ParticleCollisionEvent>();

    [Inject(Id = PuddleType.Android)]
    private BasePool<PuddleFX> _puddleFXPool;

    [Inject]
    private void Construct(PuddleFXPool _puddlePool)
    {
        _puddleFXPool = _puddlePool;
    }

    public override void OnSpawned()
    {
        print("Impact spawned.");
    }

    public void OnParticleCollision(GameObject other)
    {
        int collisionCount = _particleSystem.GetCollisionEvents(other,  _collisionEvents);

        print("Collision!");

        for(int i = 0; i < collisionCount; i++)
        {
            _puddleFXPool.Spawn(_collisionEvents[i].intersection, Quaternion.Euler(_collisionEvents[i].normal));
        }
    }

    public override void OnParticleSystemStopped()
    {
        base.OnParticleSystemStopped();


    }

    public override void OnDespawned()
    {
        base.OnDespawned();

        print("Impact despawned.");
    }
}
