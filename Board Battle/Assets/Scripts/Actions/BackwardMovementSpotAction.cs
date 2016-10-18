using System;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
    public class BackwardMovementSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            var actorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>();
            var pawnMover = actorController.CurrentPawnMover;
            var statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
            statusText.text = "The pawn is moving back";

            Action[] pawnMovements = new Action[1];

            Action pawnMovement = () =>
            {

                StartCoroutine(pawnMover.Move(spotConnection => spotConnection.PreviousSpot,
                    () =>
                    {
                        var currentSpotAction = pawnMover.CurrentSpotAction;
                        if (currentSpotAction is BackwardMovementSpotAction || currentSpotAction is StartSpotAction)
                        {
                            postAction();
                        }
                        else
                        {
                            pawnMovements[0]();
                        }
                    }));
            };

            pawnMovements[0] = pawnMovement;
            pawnMovement();
        }
    }
}
