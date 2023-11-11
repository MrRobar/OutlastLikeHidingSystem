using System;
using UnityEngine;

public class HidingSpotSeeker : MonoBehaviour
{
    [SerializeField] private LayerMask _hidingSpotsMask;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private UnderBedController _underBedController;
    [SerializeField] private HidingSpot _currentHidingSpot;
    private RaycastHit _hit;

    public event Action<HidingSpot> OnHidingSpotFound;

    private void Update()
    {
        ScanForHidingSpot();
        HandleHiding();
    }

    private void ScanForHidingSpot()
    {
        Physics.Raycast(transform.position, transform.forward, out _hit, 1.3f, _hidingSpotsMask);
    }

    private void HandleHiding()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var defaultState = _animator.GetCurrentAnimatorStateInfo(0).IsName("Default_Idle");
            if (_hit.transform != null && defaultState)
            {
                var hidingSpot = _hit.transform.gameObject.GetComponent<HidingSpot>();
                _currentHidingSpot = hidingSpot;
                hidingSpot.UpdateHideoutState(_controller.IsHiding, _controller.GetComponent<Animator>());
                OnHidingSpotFound?.Invoke(hidingSpot);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}