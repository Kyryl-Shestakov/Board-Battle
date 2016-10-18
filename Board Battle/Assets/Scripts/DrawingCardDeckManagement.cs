using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Random = UnityEngine.Random;
// ReSharper disable PossibleNullReferenceException

public class DrawingCardDeckManagement : MonoBehaviour
{
    public int CardCount;
    public float CardElevation;
    public GameObject CardPrefab;
    //public GameObject PlayerHand;
    //public GameObject OpponentHand;
    public Text Status;

    private Stack<Card> _drawingCardDeck;

    private const int MinStatRank = 1;
    private const int MaxStatRank = 20;
    private const int MinStepCount = 1;
    private const int MaxStepCount = 10;

    public event EventHandler CardsDealt;

    //private DoubleExecutionCompletionEventResolution _resolver;

    protected virtual void OnCardsDealt()
    {
        //_resolver.Resolve(() =>
        //{
        //    if (CardsDealt != null) CardsDealt(this, EventArgs.Empty);
        //});
        CardsDealt(this, EventArgs.Empty);
    }

    void Awake()
    {
        _drawingCardDeck = FormCardDeck();

        CardsDealt += (sender, args) =>
        {
            Delegate[] handlers = CardsDealt.GetInvocationList();
            handlers.ToList().ForEach(d =>
            {
                CardsDealt -= d as EventHandler;
            });
            CardsDealt += (o, eventArgs) =>
            {
                GameObject.FindGameObjectWithTag("Interface").transform.FindChild("Roll Button").gameObject.SetActive(true);
                SetStatusText("Roll of the dice for Player");
            };
        };
        //_resolver = new DoubleExecutionCompletionEventResolution(() => {});
        SetStatusText("Deal the cards");
    }

    Stack<Card> FormCardDeck()
    {
        Stack<Card> cardDeck = new Stack<Card>();

        for (int i = 0; i < CardCount; ++i)
        {
            Card card = CreateCard();
            cardDeck.Push(card);
        }

        return cardDeck;
    }

    Card CreateCard()
    {
        int airRank = Random.Range(MinStatRank, MaxStatRank + 1);
        int earthRank = Random.Range(MinStatRank, MaxStatRank + 1);
        int fireRank = Random.Range(MinStatRank, MaxStatRank + 1);
        int waterRank = Random.Range(MinStatRank, MaxStatRank + 1);
        int forwardStepCount = Random.Range(MinStepCount, MaxStepCount + 1);
        int backwardStepCount = Random.Range(MinStepCount, MaxStepCount + 1);

        Card card = new Card(airRank, earthRank, fireRank, waterRank, forwardStepCount, backwardStepCount);

        return card;
    }

    GameObject GenerateCardGameObject(Card card)
    {
        var position = new Vector3(transform.position.x, transform.position.y + CardElevation, transform.position.z);
        var cardGameObject = Instantiate(CardPrefab, position, CardPrefab.transform.rotation) as GameObject;
        cardGameObject.GetComponent<CardManagement>().FormCardStats(card);
        return cardGameObject;
    }

    public void Deal()
    {
        //GameObject.FindGameObjectWithTag("Interface").transform.FindChild("Deal Button").gameObject.SetActive(false);
        SetStatusText("The cards are being dealt");

        var playerHandCardHoldingManager = GameObject.Find("Player Hand").GetComponent<CardHoldingManagement>();
        var opponentHandCardHoldingManager = GameObject.Find("Opponent Hand").GetComponent<CardHoldingManagement>();

        //The cards to a Player and Opponent are dealt simultaneously but one at a time three times
        //After that an event CardsDealt is triggered
        DealCardTo(playerHandCardHoldingManager, () =>
        {
            DealCardTo(playerHandCardHoldingManager, () =>
            {
                DealCardTo(playerHandCardHoldingManager, OnCardsDealt);
            });
        });
        DealCardTo(opponentHandCardHoldingManager, () =>
        {
            DealCardTo(opponentHandCardHoldingManager, () =>
            {
                DealCardTo(opponentHandCardHoldingManager, OnCardsDealt);
            });
        });
    }

    public void DealCardTo(CardHoldingManagement cardHoldingManager, Action nextAction)
    {
        if (_drawingCardDeck.Count == 0)
        {
            //GetComponent<MeshRenderer>().enabled = false;
            var discardedCardDeckManager =
                    GameObject.Find("Discarded Card Deck").GetComponent<DiscardedCardDeckManagement>();
            var shuffledDeck = discardedCardDeckManager.EmptyTheDeck();

            shuffledDeck.ForEach(card => _drawingCardDeck.Push(card));
            //GetComponent<MeshRenderer>().enabled = true;
        }

        if (_drawingCardDeck.Count == 0)
        {
            _drawingCardDeck.Push(CreateCard());
        }

        var cardBase = _drawingCardDeck.Pop();
        var cardGameObject = GenerateCardGameObject(cardBase);
        cardHoldingManager.TakeTheCard(cardGameObject, nextAction);
    }

    void SetStatusText(string text)
    {
        Status.text = text;
    }
}
