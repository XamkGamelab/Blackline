using UnityEngine;
using Zenject;

public class DemoSurface : MonoBehaviour, IDamageble
{
    [SerializeField]
    private SurfaceMaterial _surfaceMaterial;

    private BulletImpactFXPool _bulletImpactFXPool;

    public SurfaceMaterial SurfaceMaterial => _surfaceMaterial;

    [Inject]
    public void Construct(BulletImpactFXPool impactFXPool)
    {
        _bulletImpactFXPool = impactFXPool;
    }

    public void ApplyDamage(float damage, float armorPenetration)
    {

    }
}
