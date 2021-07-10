using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction Handler", menuName = "Handlers/Interaction")]
public class InteractionHandler : ScriptableObject
{
    public Action<Interaction> onInteractionInRange, onInteractionOutOfRange, onItemPickup, onUseVisualChange;
    public Action onPlayerEnteredWithItem;
    private Interaction _mostRecentInteraction;

    public void EnterInteractionRange(Interaction inter, bool playerEquipped = false)
    {
        _mostRecentInteraction = inter;
        
        if(playerEquipped)
            onPlayerEnteredWithItem?.Invoke();
            
        onInteractionInRange?.Invoke(inter);
    }

    public void ExitInteractionRange(Interaction inter)
    {
        if (_mostRecentInteraction == inter)
        {
            onInteractionOutOfRange?.Invoke(inter);
            _mostRecentInteraction = null;
        }
    }

    public void ReturnItem(PickupInteraction inter)
    {
        if (_mostRecentInteraction is UseInteraction)
        {
            _mostRecentInteraction.type = Interaction.InteractionType.Check;
            onUseVisualChange?.Invoke(_mostRecentInteraction);
        }

        inter.ReturnInteraction();
        
    }

    public void PickupItem(Interaction item)
    {
        if (item.IsUnityNull() == false)
            onItemPickup?.Invoke(item);
    }

    public void UseItemOnInteraction(Interaction equippedInteraction)
    {
        if (_mostRecentInteraction is UseInteraction)
        {
            var use = (UseInteraction) _mostRecentInteraction;
            if (use.requiredInteraction == equippedInteraction)
                use.HasEquipment(equippedInteraction);
        }
    }

    public void Interact()
    {
        if (_mostRecentInteraction.IsUnityNull() == false)
        {
            _mostRecentInteraction.Interact();
        }
    }
}