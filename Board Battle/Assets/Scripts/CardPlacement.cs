using System;
using UnityEngine;
using Utility.CardUtility;

public class CardPlacement : MonoBehaviour
{
    public float CardWidth;
    protected float CardHalfWidth;
    public float HandWidth;
    protected float HandHalfWidth;
    public float CardElevationOverHand;
    protected CardRotationResolution CardRotationResolver;

    void Awake()
    {
        CardHalfWidth = CardWidth/2.0f;
        HandHalfWidth = HandWidth/2.0f;
        CardRotationResolver = GetComponent<CardRotationResolution>();
    }

    //public void PlaceTheCard(GameObject card)
    //{
    //    CardHolder.TakeTheCard(card);
    //}

    public Vector3 DetermineReceivedCardPlace(int cardCount)
    {
        float offset = cardCount * CardWidth + CardHalfWidth - HandHalfWidth;
        Vector3 positionInHand = new Vector3(transform.position.x + offset, transform.position.y + CardElevationOverHand, transform.position.z);
        return positionInHand;
    }

    public void PlaceReceivedCard(CardMovement cardMover, int handCardCount, Action previousAction, Action nextAction)
    {
        var cardPositionInHand = DetermineReceivedCardPlace(handCardCount);

        Action<Transform> postAction = cardTransform =>
        {
            //card.transform.Rotate(Quaternion.identity);
            //card.transform.eulerAngles = frontFlipRotation;
            CardRotationResolver.Rotate(cardTransform);
            nextAction();
        };

        previousAction();
        StartCoroutine(cardMover.Move(cardPositionInHand, postAction));
    }
}
