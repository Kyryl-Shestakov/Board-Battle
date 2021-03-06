﻿using UnityEngine;

namespace Utility.CardUtility
{
    public class ReturnCardPicking : CardPicking
    {
        public override void PickTheCard(GameObject card)
        {
            var cardHoldingManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>().CurrentHandManager;
            cardHoldingManager.PickTheCard(card);
        }
    }
}
