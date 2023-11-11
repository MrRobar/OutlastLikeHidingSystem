using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _sprintSpeed = 5f;
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private float _checkerRadius = 0.3f;
    [SerializeField] private bool _isCrouching = false;
    [SerializeField] private bool _isHiding = false;
    private Vector3 _moveVector = Vector3.zero;
    private float _gravityValue = -9.81f;
    private float _initialControllerHeight = 0f, _initialRendererHeight = 0f, _initialSpeed = 0f;
    private Vector3 _playerVelocity = Vector3.zero;
    private bool _isGrounded = false;

    public bool IsHiding
    {
        get => _isHiding;
    }

    private void Start()
    {
        _initialControllerHeight = _controller.height;
        _initialRendererHeight = transform.localScale.y;
        _initialSpeed = _speed;
    }

    private void Update()
    {
        HandleMovement();
        HandleSprint();
        HandleJumping();
        HandleCrouching();
    }

    private void HandleCrouching()
    {
        if (_isHiding)
        {
            _isCrouching = false;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isHiding)
        {
            _isCrouching = !_isCrouching;
            _speed = _isCrouching ? _speed / 2 : _initialSpeed;
        }
        
        var cHeight = _controller.height;
        cHeight = _isCrouching ? _initialControllerHeight / 2 : _initialControllerHeight;
        _controller.height = cHeight;
        
        var localScale = transform.localScale;
        localScale.y = _isCrouching
            ? _initialRendererHeight / 2
            : _initialRendererHeight;
        transform.localScale = localScale;
        
    }

    private void HandleMovement()
    {
        _moveVector = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        _controller.Move(_moveVector * (_speed * Time.deltaTime));
    }

    private void HandleSprint()
    {
        if (_isCrouching || _isHiding)
        {
            return;
        }
        _speed = Input.GetKey(KeyCode.LeftShift) ? _sprintSpeed : _initialSpeed;
    }

    private void HandleJumping()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, _checkerRadius, _groundMask);

        if (_isGrounded && _playerVelocity.y <= 0f)
        {
            _playerVelocity.y = 0f;
        }
        else
        {
            _playerVelocity.y += _gravityValue * Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_isCrouching)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
        }

        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void SetHidingState() // Called via animation events on hiding related animations
    {
        _isHiding = !_isHiding;
        //_speed = _isHiding ? 0f : _initialSpeed;
        _controller.enabled = !_isHiding;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_groundChecker.transform.position, _checkerRadius);
    }
}