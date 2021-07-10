using System;
using UnityEngine;

public class UseInteraction : Interaction
{
    public Interaction requiredInteraction;
    public override void Interact()
    {
        base.Interact();
        if(type == InteractionType.Use)
            handler.UseItemOnInteraction(this);
        Debug.Log($"Interacted with: {InteractionName}.");
    }

    public void HasEquipment(Interaction reqItem)
    {
        Debug.Log($"Used {reqItem.InteractionName} on {InteractionName} ");
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerInteractor>();

            bool playerEquipped = player.equippedItem != null;
            type = playerEquipped ? InteractionType.Use : InteractionType.Check;
                
            handler.EnterInteractionRange(this, playerEquipped);
            onEnter?.Invoke();
        }
    }
    
}