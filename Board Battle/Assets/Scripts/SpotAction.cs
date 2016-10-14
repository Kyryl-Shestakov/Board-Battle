using System;
using UnityEngine;

public abstract class SpotAction : MonoBehaviour
{
    protected ActorControl ActorController; //TODO: figure out why is it null

    //void Awake()
    //{
    //    ActorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>();
    //}
    public abstract void PerformAction(Action postAction);
}
