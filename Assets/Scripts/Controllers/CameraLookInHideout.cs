using UnityEngine;

public class CameraLookInHideout : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _sensitivity = 200f;
    [SerializeField] private float _minY, _maxY;

    private float xRotate = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleCameraLook();
    }

    private void HandleCameraLook()
    {
        xRotate -= Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
        var ver = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        
        xRotate = Mathf.Clamp(xRotate, _minY, _maxY);
        transform.eulerAngles = new Vector3(xRotate, transform.eulerAngles.y, transform.eulerAngles.z);
        
        _target.transform.Rotate(Vector3.up, ver);
    }
}