using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    
    public bool NeedToPlayAnim { get; set; }
    public bool IsOccupied { get; set; }

    protected Animator PlayerAnimator;

    public virtual void UpdateHideoutState(bool isHiding, Animator playerAnimator)
    {
        PlayerAnimator = playerAnimator;
        IsOccupied = !isHiding;
    }
}