using UnityEngine;

public class CardPlacement : MonoBehaviour
{
    public const int CardWidth = 8;
    public const int CardHalfWidth = CardWidth / 2;
    public const int HandWidth = 64;
    public const int HandHalfWidth = HandWidth/2;
    public const float CardElevationOverHand = 0.01f;

    protected CardHoldingManagement CardHolder;

    void Awake()
    {
        CardHolder = GetComponent<CardHoldingManagement>();
    }

    //public void PlaceTheCard(GameObject card)
    //{
    //    CardHolder.TakeTheCard(card);
    //}

    public Vector3 DetermineCardPlace()
    {
        int offset = CardHolder.CardCount * CardWidth + CardHalfWidth - HandHalfWidth;
        Vector3 positionInHand = new Vector3(transform.position.x + offset, transform.position.y + CardElevationOverHand, transform.position.z);
        return positionInHand;
    }
}
