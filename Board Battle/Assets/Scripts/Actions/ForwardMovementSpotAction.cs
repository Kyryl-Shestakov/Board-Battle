using System;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
    public class ForwardMovementSpotAction : SpotAction
    {
        public bool IsLastWhiteSpot;

        public override void PerformAction(Action postAction)
        {
            var actorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>();
            var pawnMover = actorController.CurrentPawnMover;
            
            Action[] pawnMovements = new Action[1];
            
            Action pawnMovement = () =>
            {

                StartCoroutine(pawnMover.Move(spotConnection => spotConnection.NextSpot,
                    () =>
                    {
                        if (pawnMover.CurrentSpotAction is ForwardMovementSpotAction)
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

            if (!IsLastWhiteSpot)
            {
                var statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
                statusText.text = "The pawn is moving to a next white spot";
                pawnMovement();
            }
            else
            {
                postAction();
            }
        }
    }
}
