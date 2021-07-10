using UnityEngine;

public class UseInteraction : Interaction
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log($"Interacted with: {InteractionName}.");
    }
}