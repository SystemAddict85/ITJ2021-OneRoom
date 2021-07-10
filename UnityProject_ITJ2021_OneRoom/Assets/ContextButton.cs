using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class ContextButton : MonoBehaviour
{
    private Image _image;
    [SerializeField] private InteractionHandler handler;
    private RectTransform _rect;
    private Animator _anim;
    private PlayerInput _input;

    private bool _hasControl = false;
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _anim = GetComponentInChildren<Animator>();
        _rect = GetComponent<RectTransform>();
        _image = GetComponentInChildren<Image>();
        
        handler.onInteractionInRange += EnterInteractionRange;
        handler.onInteractionOutOfRange += ExitInteractionRange;
        ToggleButton(false);
    }

    private void EnterInteractionRange(Interaction interact)
    {
        _rect.SetParent(interact.transform);
        _rect.localPosition = Vector3.up;
        _anim.SetFloat("contextEnum", (int)interact.type);
        ToggleButton(true);
    }

    private void ExitInteractionRange(Interaction interact)
    {
        _rect.SetParent(null);
        ToggleButton(false);
    }

    private void ToggleButton(bool contextEnabled)
    {
        _image.enabled = contextEnabled;
        _hasControl = contextEnabled;
    }

    private void Update()
    {
        if (_hasControl == false)
            return;

        if (_input.Interact)
            handler.UseInteraction();
    }
}