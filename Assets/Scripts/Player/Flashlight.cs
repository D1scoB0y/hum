using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Light _flashlight;

    private InputAction _toggleFlashlight;

    private void Awake()
    {
        _toggleFlashlight = _playerInput.actions.FindAction("ToggleFlashlight");
    }

    private void OnEnable()
    {
        _toggleFlashlight.performed += ToggleFlashlight;   
    }

    private void OnDisable()
    {
        _toggleFlashlight.performed -= ToggleFlashlight;   
    }

    private void ToggleFlashlight(InputAction.CallbackContext _)
    {
        _flashlight.enabled = !_flashlight.enabled;
    }
}
