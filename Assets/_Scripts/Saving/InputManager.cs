using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionAsset InputActionAsset;
    public static InputActionAsset inputActions;

    private void Awake()
    {
        inputActions = InputActionAsset;
    }
}
