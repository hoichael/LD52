using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class pl_wep_base : MonoBehaviour
{
    [Header("BASE --- REFS")]
    [SerializeField] protected pl_wep_refs refs;
    [SerializeField] protected dmg_info dmgInfo;
    [SerializeField] GameObject dropPrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected AudioSource audioSource;

    [Header("BASE --- RECOIL")]
    [SerializeField] Vector3 recoilTargetPos;
    [SerializeField] float recoilAnimSpeed;
    [SerializeField] AnimationCurve recoilAnimCurve;

    [Header("BASE --- VALUES")]
    [SerializeField] float fireRate;

    float currentRecoilAnimFactor = 1;
    public Transform ikTargetHolderLeft, ikTargetHolderRight;
    public string ID;
    bool canShoot;
    Vector3 defaultPos;

    private void Update()
    {
        if(currentRecoilAnimFactor != 1)
        {
            HandleRecoiAnim();
        }

        if(canShoot && Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
            currentRecoilAnimFactor = 0;
            transform.localPosition = recoilTargetPos;
            StartCoroutine(HandleFirerate());
        }
    }

    public void Equip()
    {
        this.gameObject.SetActive(true);
        canShoot = true;
        defaultPos = transform.localPosition;
        currentRecoilAnimFactor = 1;
    }

    public void Drop()
    {
        Rigidbody rb = Instantiate(dropPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce((Vector3.up + refs.camHolderTrans.forward.normalized) * 2.5f, ForceMode.Impulse);
        rb.AddTorque(refs.camHolderTrans.forward, ForceMode.Impulse);

        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }

    protected virtual void Shoot() {}

    protected virtual void HandleRecoiAnim()
    {
        currentRecoilAnimFactor = Mathf.MoveTowards(currentRecoilAnimFactor, 1, recoilAnimSpeed * Time.deltaTime);

        transform.localPosition = Vector3.Lerp(
            recoilTargetPos,
            defaultPos,
            recoilAnimCurve.Evaluate(currentRecoilAnimFactor)
            );
    }

    private IEnumerator HandleFirerate()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
