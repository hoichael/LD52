using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class pl_wep_base : MonoBehaviour
{
    [Header("BASE --- REFS")]
    [SerializeField] protected pl_wep_refs refs;
    [SerializeField] protected dmg_info dmgInfo;
    //[SerializeField] GameObject dropPrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] GameObject crosshair;
    public GameObject uiObject;
    public ui_rotate uiRotator;
    public Transform pivotTrans;

    [Header("BASE --- RECOIL")]
    [SerializeField] float recoilPosZ;
    [SerializeField] float recoilAnimSpeed;
    [SerializeField] AnimationCurve recoilAnimCurve;

    [Header("BASE --- VALUES")]
    [SerializeField] float fireRate;
    [SerializeField] Vector3 hiddenRot;
    [SerializeField] float equipAnimSpeed;

    float currentRecoilAnimFactor = 1;
    public Transform ikTargetHolderLeft, ikTargetHolderRight;
    public string ID;
    bool canShoot;
    public Vector3 defaultPos;
    public Vector3 pivotTransDefaultPos;
    public float animWeightMult; // the lower the value (0-1) the heigher the "weight"
    float currentEquipAnimFactor = 1;


    private void Update()
    {
        if (currentEquipAnimFactor != 1)
        {
            HandleEquipAnim();
        }
        else
        {
            if (currentRecoilAnimFactor != 1)
            {
                HandleRecoiAnim();
            }

            if (canShoot && Input.GetKey(KeyCode.Mouse0))
            {
                Shoot();
                currentRecoilAnimFactor = 0;
                transform.localPosition = new Vector3(0, 0, recoilPosZ);
                StartCoroutine(HandleFirerate());
            }
        }
    }

    public void Equip()
    {
        ResetVisuals();

        crosshair.SetActive(true);
        this.gameObject.SetActive(true);
        canShoot = true;
        //defaultPos = transform.localPosition;

        uiObject.SetActive(true);
        uiRotator.enabled = true;

        //pivotTrans.localRotation = Quaternion.Euler(hiddenRot);
        currentEquipAnimFactor = 0;
    }

    public void Unequip()
    {
        //Rigidbody rb = Instantiate(dropPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //rb.AddForce((Vector3.up + refs.camHolderTrans.forward.normalized) * 2.5f, ForceMode.Impulse);
        //rb.AddTorque(refs.camHolderTrans.forward, ForceMode.Impulse);

        StopAllCoroutines();

        //transform.localPosition = defaultPos;
        crosshair.SetActive(false);
        this.gameObject.SetActive(false);

        uiRotator.enabled = false;
    }

    protected virtual void Shoot() {}

    protected virtual void HandleRecoiAnim()
    {
        currentRecoilAnimFactor = Mathf.MoveTowards(currentRecoilAnimFactor, 1, recoilAnimSpeed * Time.deltaTime);

        transform.localPosition = Vector3.Lerp(
            new Vector3(defaultPos.x, defaultPos.y, recoilPosZ),
            defaultPos,
            recoilAnimCurve.Evaluate(currentRecoilAnimFactor)
            );
    }

    protected virtual void ResetVisuals()
    {
        transform.localPosition = defaultPos;
        currentRecoilAnimFactor = 1;
    }

    private void HandleEquipAnim()
    {
        currentEquipAnimFactor = Mathf.MoveTowards(currentEquipAnimFactor, 1, equipAnimSpeed * Time.deltaTime);
        pivotTrans.localRotation = Quaternion.Euler(Vector3.Lerp(
            hiddenRot,
            Vector3.zero,
            currentEquipAnimFactor
            ));
    }

    private IEnumerator HandleFirerate()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
