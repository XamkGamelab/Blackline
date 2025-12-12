using UnityEngine;

public class AmmoPickup : BaseItemPickup
{
    [SerializeField]
    private BaseAmmoDataSheet _ammoAddType;
    [SerializeField]
    private int _ammoAddAmount;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInventory>(out PlayerInventory playerInventory))
        {
            playerInventory.AddAmmo(_ammoAddType, _ammoAddAmount);
            gameObject.SetActive(false);
        }
    }
}
