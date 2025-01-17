using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;
    public Transform cameraPivot;
    public Camera cameraObject;
    public GameObject player;

    private Vector3 _cameraFollowVelocity = Vector3.zero;
    private Vector3 _targetPosition;
    
    [Header("Camera Speed")]
    [SerializeField]
    private float cameraSmoothTime = 0.2f;

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    public void HandleAllCameraMovement()
    {
        FollowPlayer();
        //Rotate camera
    }

    private void FollowPlayer()
    {
        _targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref _cameraFollowVelocity, cameraSmoothTime* Time.deltaTime);
        transform.position = _targetPosition;
    }
}
