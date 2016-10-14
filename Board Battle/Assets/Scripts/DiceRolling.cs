using System;
using System.Collections.Generic;
using Linking;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceRolling : MonoBehaviour
{
    public const int MinStepCount = 1;
    public const int MaxStepCount = 6;

    public Text Status;
    public GameObject CurrentPawn;

    public int RollTheDie()
    {
        return Random.Range(MinStepCount, MaxStepCount + 1);
    }

    public void GoForth()
    {
        GameObject.FindGameObjectWithTag("Interface").transform.FindChild("Roll Button").gameObject.SetActive(false);
        SetStatusText("The pawn is moving");

        int stepCount = RollTheDie();
        PawnMovement pawnMover = CurrentPawn.GetComponent<PawnMovement>();
        Queue<Action> queue = new Queue<Action>();
        Action pawnMovement = () =>
        {
            StartCoroutine(pawnMover.Move(spotConnection => spotConnection.NextSpot,
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

                    if (queue.Count != 0)
                    {
                        Action postAction = queue.Dequeue();
                        postAction();
                    }
                    else
                    {
                        CurrentPawn.GetComponent<SpotAction>().PerformAction();
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

    void SetStatusText(string text)
    {
        Status.text = text;
    }
}
