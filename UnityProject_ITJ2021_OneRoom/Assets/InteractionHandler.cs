using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction Handler", menuName = "Handlers/Interaction")]
public class InteractionHandler : ScriptableObject
{
    public Action<Interaction> onInteractionInRange, onInteractionOutOfRange;
    private Interaction _mostRecentInteraction;

    public void EnterInteractionRange(Interaction inter)
    {
        _mostRecentInteraction = inter;
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

    public void UseInteraction()
    {
        if (_mostRecentInteraction != null)
        {
            _mostRecentInteraction.Interact();
        }
    }
}