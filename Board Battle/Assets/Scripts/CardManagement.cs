using System.Collections.Generic;
using UnityEngine;
using Utility;

public class CardManagement : MonoBehaviour
{
    public int CardCount;
    private List<Card> _drawingCardDeck;

    private const int MinStatRank = 1;
    private const int MaxStatRank = 20;

	void Awake()
	{
        _drawingCardDeck = FormCardDeck();
	}

    List<Card> FormCardDeck()
    {
        List<Card> cardDeck = new List<Card>();

        for (int i = 0; i < CardCount; ++i)
        {
            int airRank = Random.Range(MinStatRank, MaxStatRank + 1);
            int earthRank = Random.Range(MinStatRank, MaxStatRank + 1);
            int fireRank = Random.Range(MinStatRank, MaxStatRank + 1);
            int waterRank = Random.Range(MinStatRank, MaxStatRank + 1);
            

            Card card = new Card(airRank, earthRank, fireRank, waterRank);

            cardDeck.Add(card);
        }

        return cardDeck;
    }
}
