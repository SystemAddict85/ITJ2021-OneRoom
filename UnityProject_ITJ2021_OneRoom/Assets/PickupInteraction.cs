using System;
using DG.Tweening;
using UnityEngine;

public class PickupInteraction : Interaction
{
    private Vector3 _returnPosition;

    private Transform _originalParent;

    private SpriteRenderer _rend;
    [HideInInspector]
    public Sprite buttonSprite;
    private void Awake()
    {
        _rend = GetComponent<SpriteRenderer>();
        type = InteractionType.Pickup;
        _returnPosition = transform.position;
        buttonSprite = _rend.sprite;
        _originalParent = transform.parent;
        ToggleReadyToInteract(true);
    }

    public void UpdateReturnPosition() => _returnPosition = transform.position;

    public override void Interact()
    {
        base.Interact();
        handler.PickupItem(this);
    }
    
    public void ToggleSprite(bool spriteEnable)
    {
        _rend.enabled = spriteEnable;
    }

    public void ReturnInteraction()
    {
        transform.DOKill();
        transform.DOMove(_returnPosition, 1f).OnComplete(()=>ToggleReadyToInteract(true));
        transform.parent = _originalParent;
    }
}