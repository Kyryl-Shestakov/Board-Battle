using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorControl : MonoBehaviour
{
    public PawnMovement CurrentPawnMover;
    public CardHoldingManagement CurrentHandManager;
    public DrawingCardDeckManagement DrawingCardDeckManager;

    public Text Status;
    public DiceRolling DiceRoller;

    public void GoForth()
    {
        //GameObject.FindGameObjectWithTag("Interface").transform.FindChild("Roll Button").gameObject.SetActive(false);
        SetStatusText("The pawn is moving");

        int stepCount = DiceRoller.RollTheDie();
        var queue = new Queue<Action>();
        Action pawnMovement = () =>
        {
            StartCoroutine(CurrentPawnMover.Move(spotConnection => spotConnection.NextSpot,
                () =>
                {
                    //try
                    //{
                    //    Action postAction = queue.Dequeue();
                    //    postAction();
                    //}
                    //catch (InvalidOperationException)
                    //{
                    //    CurrentPawn.GetComponent<SpotAction>().PerformAction();
                    //}

                    if (queue.Count != 0) //If there are movements left perform them
                    {
                        Action postAction = queue.Dequeue();
                        postAction();
                    }
                    else //Perform actions defined by the landing spot
                    {
                        CurrentPawnMover.CurrentSpotAction.PerformAction(() => { });
                    }
                }));
        };

        for (int i = 0; i < stepCount; ++i)
        {
            queue.Enqueue(pawnMovement);
        }

        Action action = queue.Dequeue();
        action();
    }

    protected void SetStatusText(string text)
    {
        Status.text = text;
    }
}