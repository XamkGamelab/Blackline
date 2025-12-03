using UnityEngine;

public class MeleeWeaponEvents : MonoBehaviour
{
    [SerializeField]
    private MeleeWeapon _meleeWeapon;

    public void SwingEvent()
    {
        print("Whoosh!!");

        _meleeWeapon.SwingWeapon();
    }
}
