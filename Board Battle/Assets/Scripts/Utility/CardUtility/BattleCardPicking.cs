using UnityEngine;

namespace Utility.CardUtility
{
    public class BattleCardPicking : CardPicking
    {
        public override void PickTheCard(GameObject card)
        {
            var cardHoldingManager = GameObject.Find("Player Hand").GetComponent<CardHoldingManagement>();
            cardHoldingManager.PickTheCard(card);
        }
    }
}
