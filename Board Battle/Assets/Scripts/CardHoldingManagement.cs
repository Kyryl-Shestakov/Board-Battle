using System;
using UnityEngine;
using System.Collections.Generic;

public class CardHoldingManagement : MonoBehaviour
{
    public int CardCapacity;
    private List<GameObject> _cards;
    protected CardPlacement CardPlaceManager;

    void Awake()
    {
        _cards = new List<GameObject>();
        CardPlaceManager = GetComponent<CardPlacement>();
    }

    public int CardCount
    {
        get { return _cards.Count; }
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
}
