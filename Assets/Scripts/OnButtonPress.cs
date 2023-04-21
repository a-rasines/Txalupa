using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class OnButtonPress : MonoBehaviour
{
    [Tooltip("Actions to check")]
    public UnityEngine.InputSystem.InputAction action;

    // When the button is pressed
    public UnityEvent OnPress = new UnityEvent();

    private void Awake()
    {
        //action.started += Pressed;
        action.started += Pressed; ;
    }

    private void Pressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPress.Invoke();
    }

    private void OnDestroy()
    {
        action.started -= Pressed;
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }
}