using System.Collections.Generic;
using UnityEngine;

public class SurfaceImpactLibrary : MonoBehaviour
{
    public static SurfaceImpactLibrary Instance;

    [SerializeField]
    private List<SurfaceImpactData> _surfaceImpactData;

    private void Awake() => Instance = this;

    public void SpawnImpactFX(RaycastHit hit, BaseWeapon weapon)
    {

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
    public GameObject ImpactEffect;
}
