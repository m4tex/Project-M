using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionAsset InputActionAsset;
    private static InputActionAsset inputActions;

    private void Awake()
    {
        inputActions = InputActionAsset;
    }
}
