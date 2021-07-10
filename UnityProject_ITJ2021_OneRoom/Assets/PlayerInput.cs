using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : InputMono
{
    public override float Horizontal => Input.GetAxisRaw("Horizontal");
    public override float Vertical => Input.GetAxisRaw("Vertical");

    public bool Interact => Input.GetKeyDown(KeyCode.Space);

    public override bool HasInput3D()
    {
        return (Horizontal != 0f || Vertical != 0f);
    }

    public override bool HasInput2D()
    {
        return (Horizontal != 0f);
    }
}

public abstract class InputMono : MonoBehaviour
{
    public abstract float Horizontal { get; }
    public abstract float Vertical { get; }
    public abstract bool HasInput2D();
    public abstract bool HasInput3D();
}
