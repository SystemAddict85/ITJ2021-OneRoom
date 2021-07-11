using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
   [SerializeField] private GameObject frontCam;
   private PlayerInput _input;
   private bool _canSwitchCamera = false;


   private void Awake()
   {
      _input = GetComponent<PlayerInput>();
   }

   private void Update()
   {
      if (_canSwitchCamera == false)
         return;
      
      if(_input.SwitchCamera)
         frontCam.SetActive(!frontCam.activeSelf);
   }

   public void ToggleCameraControl(bool controlEnable) => _canSwitchCamera = controlEnable;
}
