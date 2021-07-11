using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventHelper : MonoBehaviour
{
    [SerializeField] private string parameterName;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void ChangeParameterName(string newName) => parameterName = newName;

    public void ChangeBooleanParameter(bool value) => _anim.SetBool(parameterName, value);
    public void ChangeIntegerParameter(int value) => _anim.SetInteger(parameterName, value);
    public void ChangeFloatParameter(float value) => _anim.SetFloat(parameterName, value);
}