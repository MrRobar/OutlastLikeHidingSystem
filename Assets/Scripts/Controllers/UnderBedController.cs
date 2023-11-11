using System;
using UnityEngine;

public class UnderBedController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Bed _currentHideout;
    private RaycastHit _hit;

    public HidingSpot CurrentHideout
    {
        get { return _currentHideout; }
        set { _currentHideout = (Bed)value ; }
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.W))
        {
            Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, 3f);
            if (_hit.collider == null)
            {
                _currentHideout.UpdateHideoutState(false, GetComponent<Animator>());
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_camera.transform.position, transform.forward);
    }
}