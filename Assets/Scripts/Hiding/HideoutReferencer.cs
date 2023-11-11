using System;
using UnityEngine;

public class HideoutReferencer : MonoBehaviour
{
    [SerializeField] private HidingSpot _currentHidingSpot;
    [SerializeField] private UnderBedController _underBedController;
    [SerializeField] private HidingSpotSeeker _hidingSpotSeeker;

    private void OnEnable()
    {
        _hidingSpotSeeker.OnHidingSpotFound += UpdateHideout;
    }

    private void OnDisable()
    {
        _hidingSpotSeeker.OnHidingSpotFound -= UpdateHideout;
    }

    private void UpdateHideout(HidingSpot spot)
    {
        if (spot.GetType() != typeof(Bed))
        {
            return;
        }
        _currentHidingSpot = spot;
        _underBedController.CurrentHideout = _currentHidingSpot;
    }
}