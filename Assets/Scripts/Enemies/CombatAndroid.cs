using UnityEngine;
using UnityEngine.AI;

public class CombatAndroid : MonoBehaviour, IDamageble
{
    [SerializeField]
    private NavMeshAgent _agent;
    
    [SerializeField]
    private SurfaceMaterial _surfaceMaterial;
    public SurfaceMaterial SurfaceMaterial => _surfaceMaterial;

    public void ApplyDamage(float damage, float armorPenetration)
    {

    }
}
