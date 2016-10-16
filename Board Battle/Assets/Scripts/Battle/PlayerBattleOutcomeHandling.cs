using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class PlayerBattleOutcomeHandling : BattleOutcomeHandling
    {
        public override void HandlePlayerWinning(int forwardStepCount, int backwardStepCount, Action postAction)
        {
            //TODO: Resolve violated DRY principle (See GoForth method)
            var pawnMover = GetComponent<PawnMovement>();
            var statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
            statusText.text = "The pawn is moving";

            var queue = new Queue<Action>();
            Action pawnMovement = () =>
            {
                StartCoroutine(pawnMover.Move(spotConnection => spotConnection.NextSpot,
                    () =>
                    {
                        if (queue.Count != 0) //If there are movements left, perform them
                        {
                            Action nextAction = queue.Dequeue();
                            nextAction();
                        }
                        else
                        {
                            //pawnMover.CurrentSpotAction.PerformAction(() =>
                            //{
                            //    GameObject.FindGameObjectWithTag("Interface")
                            //        .transform.FindChild("Roll Button")
                            //        .gameObject.SetActive(true);
                            //    statusText.text = "Roll the dice";
                            //});
                            postAction();
                        }
                    }));
            };

            for (int i = 0; i < forwardStepCount; ++i)
            {
                queue.Enqueue(pawnMovement);
            }

            Action action = queue.Dequeue();
            action();
        }

        public override void HandleOpponentWinning(int forwardStepCount, int backwardStepCount, Action postAction)
        {
            var pawnMover = GetComponent<PawnMovement>();
            var statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
            statusText.text = "The pawn is moving";

            var queue = new Queue<Action>();
            Action pawnMovement = () =>
            {
                StartCoroutine(pawnMover.Move(spotConnection => spotConnection.PreviousSpot,
                    () =>
                    {
                        if (queue.Count != 0) //If there are movements left, perform them
                        {
                            Action nextAction = queue.Dequeue();
                            nextAction();
                        }
                        else
                        {
                            //pawnMover.CurrentSpotAction.PerformAction(() =>
                            //{
                            //    GameObject.FindGameObjectWithTag("Interface")
                            //        .transform.FindChild("Roll Button")
                            //        .gameObject.SetActive(true);
                            //    statusText.text = "Roll the dice";
                            //});
                            postAction();
                        }
                    }));
            };

            for (int i = 0; i < backwardStepCount; ++i)
            {
                queue.Enqueue(pawnMovement);
            }

            Action action = queue.Dequeue();
            action();
        }

        public override void HandleTie(Action postAction)
        {
            postAction();
        }
    }
}
