using UnityEngine;

public class en_health_base : MonoBehaviour
{
    [SerializeField] protected en_info_base info;

    [SerializeField]
    protected int hpMax;

    [SerializeField]
    protected int hpCurrent;

    [SerializeField] Rigidbody hpPickupPrefab, moneyPickupPrefab; //rb type because still instantiates full prefab but doesnt require getcomponent call to get rb for drop impulse force

    protected virtual void Start()
    {
        hpCurrent = hpMax;
    }

    public virtual void HandleDamage(dmg_info dmgInfo)
    {
        hpCurrent -= dmgInfo.dmgAmount;
        print("received damage (" + dmgInfo.dmgAmount + ") remaining hp: " + hpCurrent);

        if (hpCurrent <= 0) HandleDeath(dmgInfo);
    }

    protected virtual void HandleDeath(dmg_info dmgInfo)
    {
        print("ded lel");
        HandleDrop();
    }

    private void HandleDrop()
    {
        float randomNum = Random.Range(0, 1f);
        if(randomNum < 0.2f)
        {
            InstantiatePickup(hpPickupPrefab);
        }
        else if(randomNum < 0.45f)
        {
            InstantiatePickup(moneyPickupPrefab);
        }
    }

    private void InstantiatePickup(Rigidbody pickupToInstantiate)
    {
        Rigidbody instantiatedRB = Instantiate(pickupToInstantiate, info.trans.position + Vector3.up, Quaternion.identity);
        instantiatedRB.AddForce(Vector3.up * 3f, ForceMode.Impulse);
        instantiatedRB.AddTorque((Vector3.up + Vector3.right) * 1.5f, ForceMode.Impulse);
    }
}
