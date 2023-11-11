using UnityEngine;

public class Locker : HidingSpot
{
    [SerializeField] private Animator _animator;

    public override void UpdateHideoutState(bool isHiding, Animator playerAnimator)
    {
        base.UpdateHideoutState(isHiding, playerAnimator);
        if (!isHiding)
        {
            playerAnimator.SetTrigger("Get_In_Locker");
        }
        else
        {
            playerAnimator.SetTrigger("Leave_Locker");
        }
        _animator.SetTrigger("Door_Opening");
    }
}