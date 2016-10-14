using System;
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
            var cardDeckManager = actorController.CardDeckManager;
            var cardHoldingManager = actorController.CurrentHandManager;
            cardDeckManager.DealCardTo(cardHoldingManager, postAction);
        }
    }
}
