using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class OnButtonPress : MonoBehaviour
{
    [Tooltip("Actions to check")]
    public InputAction action;
    public InputActionReference _action;

    // When the button is pressed
    public UnityEvent OnPress = new UnityEvent();

    private void Awake()
    {
        //action.started += Pressed;
        _action.action.started += Pressed; ;
    }

    private void Pressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPress.Invoke();
    }

    private void OnDestroy()
    {
        _action.action.started -= Pressed;
    }

    private void OnEnable()
    {
        _action.action.Enable();
    }

    private void OnDisable()
    {
        _action.action.Disable();
    }
}