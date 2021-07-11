using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public PickupInteraction equippedItem;

    [SerializeField] InteractionHandler handler;
    [SerializeField] private Vector3 equipOffset, bounceOffset;


    private PlayerInput _input;

    private bool _hasControl = false;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        handler.onItemPickup += EquipItem;
    }

    private void EquipItem(Interaction interaction)
    {
        if (equippedItem.IsUnityNull() == false)
            ReturnItem();

        equippedItem = (PickupInteraction) interaction;
        equippedItem.transform.parent = transform;
        equippedItem.ToggleReadyToInteract(false);
        handler.ExitInteractionRange(equippedItem);
        equippedItem.transform.DOKill();
        equippedItem.transform.DOLocalMove(equipOffset, .5f)
            .OnComplete(() => TweenPingPong(equipOffset, equipOffset + bounceOffset));
    }

    private void TweenPingPong(Vector3 from, Vector3 to)
    {
        equippedItem.transform.DOLocalMove(to, .5f).OnComplete(() => TweenPingPong(to, from));
    }

    public void ToggleControl(bool controlEnable) => _hasControl = controlEnable;

    private void Update()
    {
        if (_hasControl == false)
            return;

        if (_input.Interact && equippedItem.IsUnityNull())
            handler.Interact();
        else if (_input.Interact && equippedItem.IsUnityNull() == false)
        {
            var usedItem= handler.UseItemOnInteraction(equippedItem);
            if (usedItem)
                ReturnItem();
        }
        else if (_input.ReturnItem && equippedItem != null)
            ReturnItem();
    }

    private void ReturnItem()
    {
        handler.ReturnItem(equippedItem);
        equippedItem = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position + equipOffset, .0625f);
    }
}