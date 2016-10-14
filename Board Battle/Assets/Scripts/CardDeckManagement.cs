using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Random = UnityEngine.Random;
// ReSharper disable PossibleNullReferenceException

public class CardDeckManagement : MonoBehaviour
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
                SetStatusText("Roll the dice");
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
            int airRank = Random.Range(MinStatRank, MaxStatRank + 1);
            int earthRank = Random.Range(MinStatRank, MaxStatRank + 1);
            int fireRank = Random.Range(MinStatRank, MaxStatRank + 1);
            int waterRank = Random.Range(MinStatRank, MaxStatRank + 1);
            int forwardStepCount = Random.Range(MinStepCount, MaxStepCount + 1);
            int backwardStepCount = Random.Range(MinStepCount, MaxStepCount + 1);

            Card card = new Card(airRank, earthRank, fireRank, waterRank, forwardStepCount, backwardStepCount);

            cardDeck.Push(card);
        }

        return cardDeck;
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

        ////Rotates the card of a Player for the front to be visible
        //Action<GameObject> frontCardRotation = card => card.transform.eulerAngles = Vector3.zero;
        ////A stub that does not rotate a card for the Opponent's card to remain hidden (the back of a card is visible)
        //Action<GameObject> backCardRotation = card =>
        //{
        //    //card.transform.eulerAngles = card.transform.eulerAngles;
        //};

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
        var cardBase = _drawingCardDeck.Pop();
        var cardGameObject = GenerateCardGameObject(cardBase);
        cardHoldingManager.TakeTheCard(cardGameObject, nextAction);
    }

    void SetStatusText(string text)
    {
        Status.text = text;
    }
}
