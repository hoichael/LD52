using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nv_int_whiteboard : nv_int_base
{
    [SerializeField] nv_whiteboard whiteboardManager;
    [SerializeField] SpriteRenderer sprRenderer;
    [SerializeField] Sprite sprDefault, sprActive;

    public int_whiteboard_type type;

    private void OnEnable()
    {
        sprRenderer.sprite = sprActive;
    }

    private void OnDisable()
    {
        sprRenderer.sprite = sprDefault;
    }

    public override void HandleInteract()
    {
        base.HandleInteract();
        whiteboardManager.HandleInteract(this);
    }
}
