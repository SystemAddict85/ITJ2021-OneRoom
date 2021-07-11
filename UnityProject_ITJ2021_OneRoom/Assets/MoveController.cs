using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] protected bool canMove;
    [SerializeField] protected InputMono input;

    [SerializeField] private float moveSpeed;
    [SerializeField] private bool canMove3D;

    protected virtual void Awake()
    {
        input = GetComponent<InputMono>();
    }

    public void Toggle3DControl(bool enableMove3D) => canMove3D = enableMove3D;
    public void ToggleControl(bool control) => canMove = control;

    protected virtual void Update()
    {
        
        if (!canMove || (canMove3D == false && input.HasInput2D() == false) || (canMove3D && input.HasInput3D() == false))
        {
            NoInput();
            return;
        }

        if (canMove3D)
            Move3D();
        else if(input.Horizontal != 0f)
            Move();
    }

    protected virtual void NoInput()
    {
        
    }

    protected virtual void Move3D()
    {
        transform.position +=
            new Vector3(input.Horizontal, 0f, input.Vertical).normalized * (moveSpeed * Time.deltaTime);
    }

    protected virtual void Move()
    {
        transform.position += Vector3.right * (input.Horizontal * moveSpeed * Time.deltaTime);
    }
}