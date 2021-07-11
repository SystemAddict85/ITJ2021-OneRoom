using System;
using UnityEngine;
using UnityEngine.UI;

public class ContextButton : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image equippedImage;
    [SerializeField] private InteractionHandler handler;
    private RectTransform _rect;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _rect = GetComponent<RectTransform>();
        
        handler.onInteractionInRange += EnterInteractionRange;
        handler.onInteractionOutOfRange += ExitInteractionRange;
        handler.onUseVisualChange += UpdateVisuals;
        handler.onItemPickup += EquipItem;
        handler.onPlayerEnteredWithItem += () => ToggleEquipItem(true);

        ToggleButton(false);
        ToggleEquipItem(false);
    }

    private void UpdateVisuals(Interaction interact)
    {
        _anim.SetFloat("contextEnum", (int) interact.type);
        
        if(interact.type != Interaction.InteractionType.Use)
            ToggleEquipItem(false);
    }

    private void EquipItem(Interaction item)
    {
        var pickup = (PickupInteraction)item;
        equippedImage.sprite = pickup.buttonSprite;
    }

    private void EnterInteractionRange(Interaction interact)
    {
        _rect.SetParent(interact.transform);
        _rect.position = interact.ContextPosition;
        UpdateVisuals(interact);
        ToggleButton(true);
    }

    private void ExitInteractionRange(Interaction interact)
    {
        _rect.SetParent(null);
        ToggleButton(false);
        ToggleEquipItem(false);
    }

    private void ToggleEquipItem(bool showItem)
    {
        equippedImage.enabled = showItem;
    }

    private void ToggleButton(bool contextEnabled)
    {
        buttonImage.enabled = contextEnabled;
    }
}