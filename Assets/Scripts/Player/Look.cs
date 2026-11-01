using UnityEngine;
using UnityEngine.InputSystem;

public sealed class Look : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float sensitivity = 0.08f;
    [SerializeField] private float minPitch = -85f;
    [SerializeField] private float maxPitch = 85f;

    private float yaw;
    private float pitch;

    private PlayerInput input;

    private InputAction action;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        action = input.actions.FindAction("Look", true);

        yaw = transform.localEulerAngles.y;

        pitch = cameraPivot.localEulerAngles.x;
        if (pitch > 180f)
            pitch -= 360f;
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

        yaw += delta.x * sensitivity;
        pitch -= delta.y * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(0f, yaw, 0f);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}