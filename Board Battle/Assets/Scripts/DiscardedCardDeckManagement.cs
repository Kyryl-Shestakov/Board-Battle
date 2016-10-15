using System;
using UnityEngine;
using System.Collections.Generic;
using Utility;

public class DiscardedCardDeckManagement : MonoBehaviour
{
    public float CardElevation { get; set; }
    private Stack<Card> _discardedCardDeck;
    
    void Awake()
    {
        _discardedCardDeck = new Stack<Card>();
        CardElevation = GameObject.Find("Drawing Card Deck").GetComponent<DrawingCardDeckManagement>().CardElevation;
    }

    public void ReceiveCard(GameObject card, Action nextAction)
    {
        GetComponent<MeshRenderer>().enabled = true;
        var cardBase = card.GetComponent<CardManagement>().CardStats;
        _discardedCardDeck.Push(cardBase);
        DestroyObject(card);
        nextAction();
    }
}
