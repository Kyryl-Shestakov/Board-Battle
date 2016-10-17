using UnityEngine;

namespace Utility.CardUtility
{
    public class TheftCardPicking : CardPicking
    {
        public override void PickTheCard(GameObject card)
        {
            var cardHoldingManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActorControl>().CurrentHandManager.OpposingCardHoldingManager;
            cardHoldingManager.PickTheCard(card);
        }
    }
}
