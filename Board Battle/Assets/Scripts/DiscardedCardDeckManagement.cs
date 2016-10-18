using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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

    public List<Card> EmptyTheDeck()
    {
        var cards = _discardedCardDeck.ToList();

        _discardedCardDeck.Clear();
        GetComponent<MeshRenderer>().enabled = false;

        var shuffledCards = cards;
        return shuffledCards;
    }
}
