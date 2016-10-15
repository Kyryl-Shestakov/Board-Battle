using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CardHoldingManagement : MonoBehaviour
{
    public CardHoldingManagement OpposingCardHoldingManager;
    public int CardCapacity;
    private List<GameObject> _cards;
    protected CardPlacement CardPlaceManager;
    private GameObject _pickedCard;

    void Awake()
    {
        _cards = new List<GameObject>();
        CardPlaceManager = GetComponent<CardPlacement>();
    }

    public int CardCount
    {
        get { return _cards.Count; }
    }

    public void IfNotFullElse(Action successAction, Action failureAction)
    {
        if (CardCount != CardCapacity)
        {
            successAction();
        }
        else
        {
            failureAction();
        }
    }

    public void TakeTheCard(GameObject card, Action nextAction)
    {
        int handCardCount = _cards.Count;
        var cardMover = card.GetComponent<CardMovement>();

        Action previousAction = () =>
        {
            //Card game object needs to be added after determination of position in hand
            //for the correct placement that depends on the number of cards before taking
            //and it preferably needs to be added before coroutine moving
            _cards.Add(card);
        };

        CardPlaceManager.PlaceReceivedCard(cardMover, handCardCount, previousAction, nextAction);
    }

    public void PickTheCard(GameObject card)
    {
        Vector3 offset = new Vector3(0.0f, 0.0f, 2.0f);

        if (_cards.Contains(card))
        {
            if (_pickedCard != null)
            {
                _pickedCard.transform.Translate(-offset);
            }
            _pickedCard = card;
            _pickedCard.transform.Translate(offset);
        }
    }

    public void PickTheFirstCard()
    {
        PickTheCard(_cards.First());
    }

    public void DiscardThePickedCard(Action nextAction)
    {
        var index = _cards.IndexOf(_pickedCard);
        _cards.Remove(_pickedCard);
        var remainingCards = _cards.Skip(index);
        CardPlaceManager.AdjustCardsInHand(remainingCards);

        var discardedCardDeckManager =
            GameObject.Find("Discarded Card Deck").GetComponent<DiscardedCardDeckManagement>();
        var cardPositionOverDiscardedCardDeck = new Vector3(discardedCardDeckManager.transform.position.x,
            discardedCardDeckManager.transform.position.y + discardedCardDeckManager.CardElevation,
            discardedCardDeckManager.transform.position.z);
        StartCoroutine(_pickedCard.GetComponent<CardMovement>()
            .Move(cardPositionOverDiscardedCardDeck,
                t => discardedCardDeckManager.ReceiveCard(t.gameObject, nextAction)));
    }
}
