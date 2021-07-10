using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.Events;

public class ExitMenu : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    private bool _hasControl = false;
    public void ToggleControl(bool controlEnable) => _hasControl = controlEnable;
    
    private bool _isMenuShowing = false;
    public void Update()
    {
        if (_hasControl == false)
            return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isMenuShowing)
            {
                onMenuClose?.Invoke();
            }
            else
            {
                onMenuOpen?.Invoke();
            }

            _isMenuShowing = !_isMenuShowing;

        }
    }

    [SerializeField] UnityEvent onMenuOpen, onMenuClose;
}
