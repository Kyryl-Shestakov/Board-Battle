using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

public class CardDeckManagement : MonoBehaviour
{
    public int CardCount;
    public GameObject CardPrefab;
    public GameObject PlayerHand;
    public GameObject OpponentHand;

    private Stack<Card> _drawingCardDeck;

    private const int MinStatRank = 1;
    private const int MaxStatRank = 20;
    private const int MinStepCount = 1;
    private const int MaxStepCount = 10;

    void Awake()
    {
        _drawingCardDeck = FormCardDeck();
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
        var cardGameObject = Instantiate(CardPrefab);
        cardGameObject.GetComponent<CardManagement>().FormCardStats(card);
        return cardGameObject;
    }

    public void Deal()
    {
        Action<GameObject> frontCardRotation = card => card.transform.eulerAngles = Vector3.zero;
        Action<GameObject> backCardRotation = card =>
        {
            //card.transform.eulerAngles = card.transform.eulerAngles;
        };

        DealCardTo(PlayerHand, frontCardRotation, () =>
        {
            DealCardTo(PlayerHand, frontCardRotation, () =>
            {
                DealCardTo(PlayerHand, frontCardRotation, () => {});
            });
        });
        DealCardTo(OpponentHand, backCardRotation, () =>
        {
            DealCardTo(OpponentHand, backCardRotation, () =>
            {
                DealCardTo(OpponentHand, backCardRotation, () => { });
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
}
