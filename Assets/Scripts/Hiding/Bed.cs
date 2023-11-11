using UnityEngine;

public class Bed : HidingSpot
{
    public override void UpdateHideoutState(bool isHiding, Animator playerAnimator = null)
    {
        base.UpdateHideoutState(isHiding, playerAnimator);
        if (!isHiding)
        {
            PlayerAnimator.SetTrigger("Get_Under_Bed");
        }
        else
        {
            if (playerAnimator == null)
            {
                Debug.Log("playeranim is null");
            }

            if (PlayerAnimator == null)
            {
                Debug.Log("PlayerAnim is null");
            }

            PlayerAnimator.SetTrigger("Get_From_Under_Bed");
        }
    }
}