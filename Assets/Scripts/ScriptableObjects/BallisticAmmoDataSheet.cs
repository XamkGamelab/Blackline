using UnityEngine;

[CreateAssetMenu(fileName = "Ballistic Ammo Data Sheet", menuName = "Data Sheets/Ammo/Create Ballistic Ammo Data Sheet")]
public class BallisticAmmoDataSheet : BaseAmmoDataSheet
{
    public override void OnHit(RaycastHit hit, GameObject source)
    {
        var damagable = hit.collider.GetComponent<IDamageble>();
        
        if(damagable != null)
        {
            DamageInfo damageInfo = new(DamageType.Ballistic, Damage, ArmorPenetration, hit.point, hit.normal);
            damagable.ApplyDamage(damageInfo);
        }
    }
}
