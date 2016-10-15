using System;
using System.Collections.Generic;
using System.Linq;
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

    public void AdjustCardsInHand(IEnumerable<GameObject> remainingCards)
    {
        ShiftCardsToTheLeft(remainingCards.Select(c => c.GetComponent<CardMovement>()).ToList());
    }

    public void ShiftCardsToTheLeft(List<CardMovement> cards)
    {
        var offset = new Vector3(-CardWidth, 0.0f, 0.0f);
        cards.ForEach(card =>
        {
            var newPosition = card.transform.position + offset;
            StartCoroutine(card.Move(newPosition, cardTransform => { }));
        });
    }

    public void MovePickedCardToDiscardedDeck(GameObject pickedCard, Action nextAction)
    {
        var discardedCardDeckManager =
            GameObject.Find("Discarded Card Deck").GetComponent<DiscardedCardDeckManagement>();
        var cardPositionOverDiscardedCardDeck = new Vector3(discardedCardDeckManager.transform.position.x,
            discardedCardDeckManager.transform.position.y + discardedCardDeckManager.CardElevation,
            discardedCardDeckManager.transform.position.z);
        StartCoroutine(pickedCard.GetComponent<CardMovement>()
            .Move(cardPositionOverDiscardedCardDeck,
                t => discardedCardDeckManager.ReceiveCard(t.gameObject, nextAction)));
    }

    public void BringPickedCardCloser(GameObject pickedCard, Vector3 newPosition, Vector3 newRotation, Action nextAction)
    {
        var mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        pickedCard.transform.SetParent(mainCameraTransform);
        pickedCard.transform.eulerAngles = newRotation;
        StartCoroutine(pickedCard.GetComponent<CardMovement>()
            .Move(newPosition,
                t =>
                {
                    t.SetParent(null);
                    nextAction();
                }));
    }
}
