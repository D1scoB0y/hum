using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (PlayerInput))]
public class Look : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _sensitivity = 0.08f;
    [SerializeField] private float _minPitch = -85f;
    [SerializeField] private float _maxPitch = 85f;

    private float _yaw;
    private float _pitch;

    private PlayerInput _playerInput;

    private InputAction action;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        action = _playerInput.actions.FindAction("Look", true);

        _yaw = transform.localEulerAngles.y;

        _pitch = _cameraTransform.localEulerAngles.x;
        if (_pitch > 180f)
            _pitch -= 360f;
    }

    private void OnEnable()
    {
        action.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        action.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        Vector2 delta = action.ReadValue<Vector2>();

        _yaw += delta.x * _sensitivity;
        _pitch -= delta.y * _sensitivity;
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);

        transform.localRotation = Quaternion.Euler(0f, _yaw, 0f);
        _cameraTransform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}
