using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
   [SerializeField] private GameObject frontCam, rotateCam;
   private PlayerInput _input;
   private bool _canSwitchCamera = false;
   private bool _canRotateCamera = false;

   public bool IsRotatingCamera { get; private set; }


   private void Awake()
   {
      _input = GetComponent<PlayerInput>();
   }

   private void Update()
   {
      if (_canSwitchCamera == false)
         return;
      
      if(_input.SwitchCamera)
      {
         frontCam.SetActive(!frontCam.activeSelf);
         return;
      }

      if (_canRotateCamera == false)
         return;

      IsRotatingCamera = _input.RotateCamera;
      rotateCam.SetActive(IsRotatingCamera);
   }
   

   public void ToggleCameraControl(bool controlEnable) => _canSwitchCamera = controlEnable;
   public void ToggleRotateCameraControl(bool rotateEnable) => _canRotateCamera = rotateEnable;
}
