using UnityEngine;
using System.Collections.Generic;

public class CardHoldingManagement : MonoBehaviour
{
    public int CardCapacity;
    private List<GameObject> _cards;

    void Start()
    {
        _cards = new List<GameObject>();
    }

    public int CardCount
    {
        get { return _cards.Count; }
    }

    public void TakeTheCard(GameObject card)
    {
        _cards.Add(card);
    }
}
