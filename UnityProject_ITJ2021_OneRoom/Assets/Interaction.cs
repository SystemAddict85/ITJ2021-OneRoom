using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interaction : MonoBehaviour
{
    [SerializeField] private string interactionName;
    public string InteractionName => interactionName == "" ? name : interactionName;

    public enum InteractionType { Check, Pickup, Use }

    public InteractionType type;
    public virtual void Interact() => onInteract?.Invoke();
    [SerializeField] protected InteractionHandler handler;

    public UnityEvent onEnter, onExit, onInteract;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            handler.EnterInteractionRange(this);
            onEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            handler.ExitInteractionRange(this);
            onExit?.Invoke();
        }
    }


}
