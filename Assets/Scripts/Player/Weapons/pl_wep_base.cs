using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class pl_wep_base : MonoBehaviour
{
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected Transform camHolderTrans;
    [SerializeField] GameObject dropPrefab;
    [SerializeField] float fireRate;
    public string ID;
    bool canShoot;

    private void Update()
    {
        if (!canShoot) return;
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
            HandleFirerate();
        }
    }

    public void Equip()
    {
        this.gameObject.SetActive(true);
        canShoot = true;
    }

    public void Drop()
    {
        //Rigidbody rb = Instantiate(dropPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //rb.AddForce(camHolderTrans.forward.normalized * 5, ForceMode.Impulse);
        //rb.AddTorque(camHolderTrans.forward, ForceMode.Impulse);

        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }

    protected virtual void Shoot() { }

    private IEnumerator HandleFirerate()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
