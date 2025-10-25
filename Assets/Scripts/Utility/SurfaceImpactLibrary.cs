using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SurfaceImpactLibrary : MonoBehaviour
{
    public static SurfaceImpactLibrary Instance;

    [SerializeField]
    private List<SurfaceImpactData> _surfaceImpactData;

    private BulletImpactFXPool _fleshImpactFXPool;

    [Inject]
    public void Construct(BulletImpactFXPool impactFXPool)
    {
        _fleshImpactFXPool = impactFXPool;
    }

    private void Awake() => Instance = this;

    public void SpawnImpactFX(RaycastHit hit)
    {
        var surface = hit.collider.GetComponent<IDamageble>();
        if (surface == null) return;

        SurfaceImpactData data = _surfaceImpactData.Find(d => d.SurfaceMaterial == surface.SurfaceMaterial);
        if (data == null) return;

        data.ImpactFXPool.Spawn(hit.point, Quaternion.LookRotation(hit.normal));
    }
}

public enum SurfaceMaterial
{
    Default,
    Concrete,
    Metal,
    Wood,
    Flesh,
    Glass,
}

[System.Serializable]
public class SurfaceImpactData
{
    public SurfaceMaterial SurfaceMaterial;
    public BulletImpactFXPool ImpactFXPool;
}
