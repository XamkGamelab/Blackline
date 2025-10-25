using UnityEngine;

public class DemoSurface : MonoBehaviour, IDamageble
{
    [SerializeField]
    private SurfaceMaterial _surfaceMaterial;

    public SurfaceMaterial SurfaceMaterial => _surfaceMaterial;

    public void ApplyDamage(float damage, float armorPenetration)
    {

    }
}
