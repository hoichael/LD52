using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nv_int_whiteboard : nv_int_base
{
    [SerializeField] nv_whiteboard whiteboardManager;
    [SerializeField] SpriteRenderer sprRenderer;
    //[SerializeField] Sprite sprDefault, sprActive;

    public int_whiteboard_type type;

    private void OnEnable()
    {
        //sprRenderer.sprite = sprActive;
        sprRenderer.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        //sprRenderer.sprite = sprDefault;
        sprRenderer.gameObject.SetActive(false);
    }

    public override void HandleInteract()
    {
        base.HandleInteract();
        whiteboardManager.HandleInteract(this);
        g_refs.Instance.sfxOneshot2D.Play(SfxType.pickup);
    }
}
