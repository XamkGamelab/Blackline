using UnityEngine;

public class HealthPickup : BaseItemPickup
{
    [SerializeField]
    private float _healthAddAmount;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.AddHealth(_healthAddAmount);
            gameObject.SetActive(false);
        }
    }
}
