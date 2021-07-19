using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/**
 * Class to handle the climbable object interactions.
 */
public class ClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (args.interactor is XRRayInteractor)
        {
            Climber.climbingHand = args.interactor as XRRayInteractor;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactor is XRRayInteractor)
        {
            if (Climber.climbingHand && Climber.climbingHand.name == args.interactor.name)
            {
                Climber.climbingHand = null;
            }
        }
    }
}
