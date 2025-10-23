using UnityEngine;

[CreateAssetMenu(fileName = "Ballistic Ammo Data Sheet", menuName = "Data Sheets/Ammo/Create Dragons Breath Ammo Data Sheet")]
public class DragonsBreathAmmoDataSheet : BaseAmmoDataSheet
{
    [SerializeField]
    private float _burnDuration;
    [SerializeField]
    private float _damageOverTime;

    public float BurnDuration => _burnDuration;
    public float DamageOverTime => _damageOverTime;

    public override void OnHit(RaycastHit hit, GameObject source)
    {
        var damagable = hit.collider.GetComponent<IDamageble>();
        
        if(damagable != null)
        {
            damagable.ApplyDamage(Damage, ArmorPenetration);
        }

        var flammable = hit.collider.GetComponent<IFlammable>();

        // If you read this, you have small pp. //
        // gotem. -Shad //
        if(flammable != null)
        {
            flammable.Ignite(_burnDuration, _damageOverTime);
        }
    }
}
