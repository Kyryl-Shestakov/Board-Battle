using UnityEngine;

namespace Utility.CardUtility
{
    public class ReturnCardPicking : CardPicking
    {
        public override void PickTheCard(GameObject card, CardHoldingManagement cardHoldingManager)
        {
            cardHoldingManager.PickTheCard(card);
        }
    }
}
