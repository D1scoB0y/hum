using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (PlayerInput), typeof (CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController _controller;
    private PlayerInput _playerInput;
    private InputAction _moveAction, _sprintAction;
    private float verticalVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Move", true);
        _sprintAction = _playerInput.actions.FindAction("Sprint", true);
    }

    private void Update()
    {
        Vector2 input = Vector2.ClampMagnitude(
            _moveAction.ReadValue<Vector2>(), 1f
        );

        Vector3 direction =
            transform.right * input.x +
            transform.forward * input.y;

        if (_controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = gravity;
        }

        verticalVelocity += gravity * Time.deltaTime;

        float speed = _sprintAction.IsPressed()
            ? sprintSpeed
            : walkSpeed;

        Vector3 velocity =
            direction * speed +
            Vector3.up * verticalVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }
}
