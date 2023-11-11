using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _sensitivity = 10f;
    private float _xRot = 0f;
    private Quaternion _localRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        var horizontal = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        var vertical = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        _xRot -= vertical;
        _xRot = Mathf.Clamp(_xRot, -85f, 85f);
        _localRot = transform.localRotation;
        _localRot = Quaternion.Euler(_xRot, _localRot.y, _localRot.z);
        transform.localRotation = _localRot;
        _target.transform.Rotate(Vector3.up, horizontal);
    }
}