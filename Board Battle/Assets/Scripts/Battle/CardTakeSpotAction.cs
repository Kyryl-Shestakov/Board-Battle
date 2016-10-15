using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Battle
{
    public class CardTakeSpotAction : SpotAction
    {
        //void Awake()
        //{
        //    ActorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>();
        //}
        public override void PerformAction(Action postAction)
        {
            var actorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>();
            var cardDeckManager = actorController.DrawingCardDeckManager;
            var cardHoldingManager = actorController.CurrentHandManager;
            cardHoldingManager.IfNotFullElse(() =>
            {
                cardDeckManager.DealCardTo(cardHoldingManager, postAction);
            }, postAction);
        }
    }
}
