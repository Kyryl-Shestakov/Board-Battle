using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
    public class OpponentBattleOutcomeHandling : BattleOutcomeHandling
    {
        public override void HandlePlayerWinning(int forwardStepCount, int backwardStepCount, Action postAction)
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
                        if (pawnMover.CurrentSpotAction is StartSpotAction)
                        {
                            postAction();
                        }
                        else
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

        public override void HandleOpponentWinning(int forwardStepCount, int backwardStepCount, Action postAction)
        {
            var pawnMover = GetComponent<PawnMovement>();
            var statusText = GameObject.FindGameObjectWithTag("Status").GetComponent<Text>();
            statusText.text = "The pawn is moving";

            if (pawnMover.NextSpotAction is FinishSpotAction)
            {
                StartCoroutine(pawnMover.Move(spotConnection => spotConnection.NextSpot,
                    () =>
                    {
                        //Handle losing situation
                        statusText.text = "You lost!";
                    }));
            }
            else
            {
                var queue = new Queue<Action>();
                Action pawnMovement = () =>
                {
                    if (!(pawnMover.NextSpotAction is FinishSpotAction))
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
                    }
                    else
                    {
                        postAction();
                    }
                };

                for (int i = 0; i < forwardStepCount; ++i)
                {
                    queue.Enqueue(pawnMovement);
                }

                Action action = queue.Dequeue();
                action();
            }
        }

        public override void HandleTie(Action postAction)
        {
            postAction();
        }
    }
}
