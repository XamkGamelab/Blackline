using System.Threading.Tasks;
using UnityEngine;

public class BulletTracerFX : MonoBehaviour, IPoolable
{
    [SerializeField]
    private TrailRenderer _trail;

    private BasePool<BulletTracerFX> _pool;

    public void OnSpawned()
    {
        _trail.Clear();
    }
    
    public void OnDespawned()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void SetPool<T>(BasePool<T> pool) where T : Component, IPoolable
    {
        _pool = pool as BasePool<BulletTracerFX>;
    }

    public void Fire(Vector3 targetPos, BaseAmmoDataSheet dataSheet)
    {
        // This is purely FX. It moves the bullet with it's "tracer" to the target pos. -Shad //
        _ = MoveTowardsTargetPos(targetPos, dataSheet);
    }

    private async Task MoveTowardsTargetPos(Vector3 targetPos, BaseAmmoDataSheet dataSheet)
    {
        Vector3 startPos = transform.position;

        // Calculate some values. -Shad //
        float distance = Vector3.Distance(startPos, targetPos);
        float travelTime = distance / dataSheet.ProjectileSpeed;
        float elapsedTime = 0f;

        // Setup a timer for the actual movement. -Shad //
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime);
            await Task.Yield();
            elapsedTime += Time.deltaTime * dataSheet.ProjectileSpeed;
        }

        // Return bullet to the pool. -Shad //
        _pool.Despawn(this);
    }
}
