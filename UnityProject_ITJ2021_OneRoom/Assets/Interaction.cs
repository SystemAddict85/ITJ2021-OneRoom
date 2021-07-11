using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interaction : MonoBehaviour
{
    [SerializeField] private string interactionName;
    public string InteractionName => interactionName == "" ? name : interactionName;
    [SerializeField] Vector3 contextPosition = new Vector3(0, 1.125f, 0f);
    public Vector3 ContextPosition => transform.position + contextPosition;

    public enum InteractionType
    {
        Check,
        Pickup,
        Use
    }

    protected bool canInteract = true;
    public InteractionType type;

    public void ToggleReadyToInteract(bool interactEnable)
    {
        canInteract = interactEnable;
        GetComponent<Collider>().enabled = interactEnable;
        
    }
    public virtual void Interact()
    {
        if (canInteract) onInteract?.Invoke();
    }

    [SerializeField] protected InteractionHandler handler;

    public UnityEvent onEnter, onExit, onInteract;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!canInteract)
            return;
        
        if ( other.CompareTag("Player"))
        {
            handler.EnterInteractionRange(this);
            onEnter?.Invoke();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            handler.ExitInteractionRange(this);
            onExit?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ContextPosition, .125f);
    }
}