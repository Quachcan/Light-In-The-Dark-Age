using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCamera : MonoBehaviour
{
    public InputManager inputManager;
    public static PlayerCamera Instance;
    public Transform cameraPivot;
    public Camera cameraObject;
    public GameObject player;

    private Vector3 _cameraFollowVelocity = Vector3.zero;
    private Vector3 _targetPosition;
    private Vector3 _cameraRotation;
    private Quaternion _targetRotation;
    
    [Header("Camera Speed")]
    [SerializeField]
    private float cameraSmoothTime = 0.2f;

    private float _lookAmountVertical;
    private float _lookAmountHorizontal;
    private float _maximumPivotAngle = 15;
    private float _minimumPivotAngle = -15f;

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void Awake()
    {
        player.GetComponent<InputManager>();
    }

    public void HandleAllCameraMovement()
    {
        FollowPlayer();
        RotateCamera();
    }

    private void FollowPlayer()
    {
        _targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref _cameraFollowVelocity, cameraSmoothTime* Time.deltaTime);
        transform.position = _targetPosition;
    }

    private void RotateCamera()
    {
        _lookAmountVertical = _lookAmountVertical + (inputManager.LookInput.x);
        _lookAmountHorizontal = _lookAmountHorizontal - (inputManager.LookInput.y);
        _lookAmountHorizontal = Mathf.Clamp(_lookAmountHorizontal, _minimumPivotAngle, _maximumPivotAngle);

        _cameraRotation = Vector3.zero;
        _cameraRotation.y = _lookAmountVertical;
        _targetRotation = Quaternion.Euler(_cameraRotation);
        _targetRotation = Quaternion.Slerp(transform.rotation, _targetRotation, cameraSmoothTime);
        transform.rotation = _targetRotation;
        
        _cameraRotation = Vector3.zero;
        _cameraRotation.x = _lookAmountHorizontal;
        _targetRotation = Quaternion.Euler(_cameraRotation);
        _targetRotation = Quaternion.Slerp(cameraPivot.localRotation, _targetRotation, cameraSmoothTime);
        cameraPivot.localRotation = _targetRotation;
    }
}
