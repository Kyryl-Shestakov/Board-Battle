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
    public GameObject PlayerHand;
    public GameObject OpponentHand;
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

        //Rotates the card of a Player for the front to be visible
        Action<GameObject> frontCardRotation = card => card.transform.eulerAngles = Vector3.zero;
        //A stub that does not rotate a card for the Opponent's card to remain hidden (the back of a card is visible)
        Action<GameObject> backCardRotation = card =>
        {
            //card.transform.eulerAngles = card.transform.eulerAngles;
        };

        //The cards to a Player and Opponent are dealt simultaneously but one at a time three times
        //After that an event CardsDealt is triggered
        DealCardTo(PlayerHand, frontCardRotation, () =>
        {
            DealCardTo(PlayerHand, frontCardRotation, () =>
            {
                DealCardTo(PlayerHand, frontCardRotation, OnCardsDealt);
            });
        });
        DealCardTo(OpponentHand, backCardRotation, () =>
        {
            DealCardTo(OpponentHand, backCardRotation, () =>
            {
                DealCardTo(OpponentHand, backCardRotation, OnCardsDealt);
            });
        });
    }

    void DealCardTo(GameObject hand, Action<GameObject> cardRotation, Action nextAction)
    {
        var cardBase = _drawingCardDeck.Pop();
        var cardGameObject = GenerateCardGameObject(cardBase);
        var cardMovement = cardGameObject.GetComponent<CardMovement>();
        var cardPositionInHand = hand.GetComponent<CardPlacement>().DetermineCardPlace();

        Action<GameObject> postAction = card =>
        {
            //card.transform.Rotate(Quaternion.identity);
            //card.transform.eulerAngles = frontFlipRotation;
            cardRotation(card);
            hand.GetComponent<CardHoldingManagement>().TakeTheCard(card);
            nextAction();
        };

        StartCoroutine(cardMovement.Move(cardPositionInHand, postAction));
    }

    void SetStatusText(string text)
    {
        Status.text = text;
    }
}
