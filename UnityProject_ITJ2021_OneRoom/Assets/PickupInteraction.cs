using System;
using DG.Tweening;
using UnityEngine;

public class PickupInteraction : Interaction
{
    private Vector3 _originalPosition;

    private Transform _originalParent;

    private SpriteRenderer _rend;
    [HideInInspector]
    public Sprite buttonSprite;
    private void Awake()
    {
        _rend = GetComponent<SpriteRenderer>();
        type = InteractionType.Pickup;
        _originalPosition = transform.position;
        buttonSprite = _rend.sprite;
        _originalParent = transform.parent;
        ReadyToInteract();
    }

    public override void Interact()
    {
        if (_hasControl == false)
            return;
        
        base.Interact();
        handler.PickupItem(this);
    }
    // protected override void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         var player = other.GetComponent<PlayerInteractor>();
    //         
    //         type = player.equippedItem != null ? InteractionType.Use : InteractionType.Check;
    //             
    //         handler.EnterInteractionRange(this);
    //         onEnter?.Invoke();
    //     }
    // }

    public void ToggleSprite(bool spriteEnable)
    {
        _rend.enabled = spriteEnable;
    }

    private bool _hasControl;
    private void ReadyToInteract()
    {
        _hasControl = true;
        GetComponent<Collider>().enabled = true;
        
    }
    
    public void ReturnInteraction()
    {
        transform.DOKill();
        transform.DOMove(_originalPosition, 1f).OnComplete(ReadyToInteract);
        transform.parent = _originalParent;
    }
}