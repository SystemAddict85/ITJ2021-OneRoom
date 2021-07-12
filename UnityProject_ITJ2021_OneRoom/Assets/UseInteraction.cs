using System;
using UnityEngine;
using UnityEngine.Events;

public class UseInteraction : Interaction
{
    public UnityEvent onWrongEquipment, onCorrectEquipment;
    public Interaction requiredInteraction;
    public override void Interact()
    {
        base.Interact();
        // if(type == InteractionType.Use)
        //     handler.UseItemOnInteraction(this);
        //Debug.Log($"Interacted with: {InteractionName}.");
    }

    public void HasEquipment(Interaction reqItem)
    {
        //Debug.Log($"Used {reqItem.InteractionName} on {InteractionName} ");
        handler.ExitInteractionRange(this);
        onCorrectEquipment?.Invoke();
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = true;
            var player = other.GetComponent<PlayerInteractor>();

            bool playerEquipped = player.equippedItem != null;
            type = playerEquipped ? InteractionType.Use : InteractionType.Check;
                
            handler.EnterInteractionRange(this, playerEquipped);
            onEnter?.Invoke();
        }
    }

   
    
}