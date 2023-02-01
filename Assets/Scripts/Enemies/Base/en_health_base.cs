using UnityEngine;

public class en_health_base : MonoBehaviour
{
    [SerializeField] protected en_info_base info;

    [SerializeField]
    protected int hpMax;

    [SerializeField]
    protected int hpCurrent;

    [SerializeField] Rigidbody hpPickupPrefab, moneyPickupPrefab; //rb type because still instantiates full prefab but doesnt require getcomponent call to get rb for drop impulse force

    private void OnEnable()
    {
        Reset();
    }

    protected virtual void Reset()
    {
        hpCurrent = hpMax;
    }

    public virtual void HandleDamage(dmg_info dmgInfo)
    {
        if (hpCurrent <= 0)
        {
            return;
        }
        hpCurrent -= dmgInfo.dmgAmount;
        //print("received damage (" + dmgInfo.dmgAmount + ") remaining hp: " + hpCurrent);

        if (hpCurrent <= 0) HandleDeath(dmgInfo);
    }

    protected virtual void HandleDeath(dmg_info dmgInfo)
    {
        //print("ded lel");
        HandleDrop();
        g_refs.Instance.gameManager.UpdateScore(info.score);
        g_refs.Instance.waveManager.HandleEnemyDeath();
        //g_refs.Instance.pool.Dispatch(PoolType.vfx_blood_expl, info.trans.position, Quaternion.identity);
    }

    private void HandleDrop()
    {
        float randomNum = Random.Range(0, 1f);
        if(randomNum < 0.04f)
        {
            //InstantiatePickup(hpPickupPrefab);
            g_refs.Instance.pool.Dispatch(PoolType.drop_health, info.trans.position + Vector3.up, Quaternion.identity);
        }
        else if(randomNum < 0.44f)
        {
            //InstantiatePickup(moneyPickupPrefab);
            g_refs.Instance.pool.Dispatch(PoolType.drop_cash, info.trans.position + Vector3.up, Quaternion.identity);
        }
    }

    //private void InstantiatePickup(Rigidbody pickupToInstantiate)
    //{
    //    Rigidbody instantiatedRB = Instantiate(pickupToInstantiate, info.trans.position + Vector3.up, Quaternion.identity);
    //    instantiatedRB.AddForce(Vector3.up * 8f, ForceMode.Impulse);
    //    instantiatedRB.AddTorque((Vector3.up + Vector3.right) * 0.4f, ForceMode.Impulse);
    //}
}
