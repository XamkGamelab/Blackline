using UnityEngine;

public class ArmorPickup : BaseItemPickup
{
    [SerializeField]
    private float _armorAddAmount;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.AddArmor(_armorAddAmount);
            gameObject.SetActive(false);
        }
    }
}
